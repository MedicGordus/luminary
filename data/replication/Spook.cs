


using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using luminary.util;

namespace luminary.data.replication;

public enum SpookTangledState : int
{
    z_error = 0,

    /// <summary>
    /// In sync.
    /// </summary>
    Entangled = 1,

    /// <summary>
    /// Out of sync with quorum.
    /// </summary>
    Drifting = 2,

    /// <summary>
    /// Drifted, but getting back in sync with Top Spook.
    /// </summary>
    Snapping = 3,

    /// <summary>
    /// Starting up or intentionally shut down.
    /// </summary>
    Dim = 4
}

/// <summary>
/// Precedence of Spooks within a Tangle.
/// </summary>
/// <remarks>
/// Valid combinations:
///     - (1+) Peers
///     
///     - Top + (0+) Bottom
///     
///     - Top + (0+) TopPeer + (0+) BottomPeer
///     
///     Other combinations may temporarily be valid while the Tangle adjusts.
/// </remarks>
public enum SpookPrecedence : int
{
    z_error = 0,

    /// <summary>
    /// This is the primary Spook. Whenever the number of Top Spooks in a Tangle is not 1, all participants vote for the Top Spook.
    /// </summary>
    Top = 10000,

    /// <summary>
    /// Part of a primary cluster within the Tangle. Peers within the sub-Tangle.
    /// </summary>
    TopPeer = 1000,

    /// <summary>
    /// This is a peer spook. There cannot be Top or Bottom spooks in a Tangle with Peers.
    /// </summary>
    Peer = 100,

    /// <summary>
    /// Part of a backup cluster within the Tangle. Peers within the sub-Tangle.
    /// </summary>
    BottomPeer = 10,

    /// <summary>
    /// This is a backup Spook, ready to vote for a new Top Spook if the Top Spook appears to go down.
    /// </summary>
    Bottom = 1
}

public class Spook
{
    protected SpookTangledState CurrentTangledState;

    /// <summary>
    /// What this Spook is configured for (not necessarily what is actual).
    /// </summary>
    protected readonly SpookPrecedence ConfiguredPrecedence;

    /// <summary>
    /// What this Spook is currently at.
    /// </summary>
    protected SpookPrecedence CurrentPrecedence;

    /// <summary>
    /// What we identify as within our Tangle.
    /// </summary>
    protected readonly string SelfAddress;

    /// <summary>
    /// List of expected Spooks in current Tangle (ALL IN LOWERCASE).
    /// </summary>
    protected readonly List<string> ExpectedTangleSpooks;

    /// <summary>
    /// List of healthy Spooks in current Tangle.
    /// </summary>
    protected HashSet<string> HealthyTangleSpooks;

    /// <summary>
    /// List of unhealthy Spooks in current Tangle.
    /// </summary>
    protected HashSet<string> UnhealthyTangleSpooks;

    /// <summary>
    /// Used when modifying or looking in HealthyTangleSpooks, UnhealthyTangleSpooks or TangleHealthUpdates
    /// </summary>
    protected readonly AsyncLock HealthyTangleSpookLock;

    /// <summary>
    /// The current Top Spook within our Tangle unless we are peers.
    /// </summary>
    protected string? TopSpookAddress;

    /// <summary>
    /// The next Spook within our Tangle to handle any integrations that fail
    ///     while we are the top node - or if another node offloaded onto us.
    /// </summary>
    protected string? OffloadSpookAddress;


    /// <summary>
    /// Lookup of each Tangled Spook's address and the last health check.
    /// 
    /// Only healthy spooks included.
    /// </summary>
    protected readonly Dictionary<string, HealthUpdate> TangleHealthUpdates;

    /// <summary>
    /// The duration that each Spook in this Tangle expects to share health updates.
    /// </summary>
    protected readonly TimeSpan HealthInterval;

    /// <summary>
    /// The duration that is waited before assuming a Spook unexpectedly left the Tangle.
    /// </summary>
    protected readonly TimeSpan HealthTimeout;

    /// <summary>
    /// Used to know when this Spook expectedly is stopped.
    /// </summary>
    protected readonly TaskCompletionSource StopSpook;

    /// <summary>
    /// Reference to the health task which runs on a parallel thread.
    /// </summary>
    protected Task? HealthUpdator;

    /// <summary>
    /// List of parallel tasks processing messages.
    /// </summary>
    /// <remarks>
    /// Be aware the health loop cleans this up and awaits these before shutting down.
    /// </remarks>
    protected readonly List<Task> MessageProcessing;

    /// <summary>
    /// Used to prevent race conditions on the MessageProcessing list.
    /// </summary>
    protected readonly AsyncLock MessageProcessingLock;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="_expectedTangleSpooks">Other spooks we expect within the Tangle (some may go dim at times).</param>
    /// <param name="_selfAddress">What we identify as within the Tangle.</param>
    /// <param name="_healthIntervalSeconds">How many seconds to wait before pinging a health update to the Tangle</param>
    /// <param name="_healthTimeoutSeconds">How many seconds to wait before other Spooks within our Tangle are assumed drifted.</param>
    public Spook(List<string> _expectedTangleSpooks, string _selfAddress, int _healthIntervalSeconds, int _healthTimeoutSeconds)
    {
        ExpectedTangleSpooks = [];
        foreach (var _deltaSpook in _expectedTangleSpooks)
        {
            ExpectedTangleSpooks.Add(_deltaSpook.ToLower());
        }
        SelfAddress = _selfAddress;
        HealthInterval = TimeSpan.FromSeconds(_healthIntervalSeconds);
        HealthTimeout = TimeSpan.FromSeconds(_healthTimeoutSeconds);

        StopSpook = new();
        CurrentTangledState = SpookTangledState.Dim;
        TopSpookAddress = null;
        OffloadSpookAddress = null;
        HealthyTangleSpooks = [_selfAddress];
        UnhealthyTangleSpooks = [];
        HealthyTangleSpookLock = AsyncLock.Create();
        TangleHealthUpdates = new Dictionary<string, HealthUpdate>() {
            {
                _selfAddress,
                new HealthUpdate(
                    new HealthStatusJson {
                        Address = SelfAddress,
                        CurrentPrecedence = CurrentPrecedence,
                        CurrentTangledState = CurrentTangledState,
                        TangleHealth = []
                    },
                    DateTime.UtcNow
                )
            }
        };

        HealthUpdator = null;

        MessageProcessing = [];
        MessageProcessingLock = AsyncLock.Create();
    }

    public void Start(string[] _tangleAddresses)
    {
        StartListening();
        HealthUpdator = Task.Run(StartHealthUpdatesAsync);
        JoinTangle(_tangleAddresses);
    }

    // Called by external class to distribute data
    public async Task ReceiveDataToDistributeAsync(string data)
    {
        await BroadcastAsync($"DATA|{data}").ConfigureAwait(false);
    }

    private void JoinTangle(string[] _tangleAddresses)
    {
        foreach (var _deltaAddress in _tangleAddresses)
        {
            SendMessage(_deltaAddress, $"JOIN|{SelfAddress}");
        }
    }

    private async Task BroadcastAsync(string message)
    {
        List<string> _cloneHealthyTangleSpooks = [];
        using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
        {
            _cloneHealthyTangleSpooks.AddRange(HealthyTangleSpooks);
        }

        foreach (var spook in _cloneHealthyTangleSpooks)
        {
            if (spook != SelfAddress)
            {
                SendMessage(spook, message);
            }
        }
    }

    private void SendMessage(string targetAddress, string message)
    {
        // Placeholder for network send (e.g., TCP/UDP)
        // In practice, this would use a network library
    }

    private void StartListening()
    {
        // Placeholder for network listener
        // Calls ProcessMessage on incoming messages
    }

    private async Task ProcessMessageAsync(string _message, string _fromAddress)
    {
        var _parts = _message.Split('|', 2);
        if (_parts.Length < 2) return;
        string _type = _parts[0];
        string _payload = _parts[1];

        Task _process;

        switch (_type)
        {
            // this is called by new spooks joining the tangle
            //  (they are not done joining until the announce)
            case "JOIN":
                _process = Task.Run(() => HandleJoinAsync(_fromAddress));
                break;

            // this is the reply to joiners, with health status
            case "WELCOME":
                _process = Task.Run(() => HandleWelcomeAsync(_payload));
                break;

            // this is called by a spook that has completed joining
            case "ANNOUNCE":
                _process = Task.Run(() => HandleAnnounceAsync(_payload).ConfigureAwait(false));
                break;

            // distribution of data payload
            case "DATA":
                HandleData(_payload);
                break;

            // if a single integration fails while the top spook is healthy it
            //  will try to offload that process to another spook.
            case "OFFLOAD":
                HandleOffload(_payload);
                break;

            // heartbeat to let the other spooks know everything is good
            case "HEALTH":
                await HandleHealthAsync(_payload).ConfigureAwait(false);
                break;

            // this is called when a spook notices something wrong with the
            //  top spook - or by top spook when going down expectedly
            case "BALLOT":
                HandleBallot(_payload);
                break;

            // called when a spook receives ballots from all healthy spooks in
            //  the tangle, on their consensus
            case "TUNE":
                HandleTune(_payload);
                break;

            // called when a spook shuts down expectedly
            //  (regardless of it's tangled state)
            case "DIMMING":
                HandleDimming(_payload);
                break;
        }

        using (await MessageProcessingLock.LockAsync().ConfigureAwait(false))
        {
            MessageProcessing.Add(_process);
        }
    }

    private async Task HandleJoinAsync(string _newSpookAddress)
    {
        if (!ExpectedTangleSpooks.Contains(_newSpookAddress.ToLower()))
        {
            return;
        }

        string _welcomePayload = (await BuildSelfHealthStatusAsync().ConfigureAwait(false)).ToJsonString() ?? "";
        SendMessage(_newSpookAddress, $"WELCOME|{_welcomePayload}");
    }

    private async Task HandleWelcomeAsync(string _payload)
    {
        HealthStatusJson? _theirHealthStatus = HealthStatusJson.Parse(_payload);

        if (_theirHealthStatus == null || _theirHealthStatus.TangleHealth == null)
        {
            return;
        }

        using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
        {
            foreach (var _deltaStatus in _theirHealthStatus.TangleHealth)
            {
                if (_deltaStatus.CurrentTangledState == SpookTangledState.Entangled)
                {
                    if (!HealthyTangleSpooks.Contains(_deltaStatus.Address))
                    {
                        HealthyTangleSpooks.Add(_deltaStatus.Address);
                    }
                }
            }
        }

        await UpdatePrecedenceAsync().ConfigureAwait(false);

        await BroadcastAsync($"ANNOUNCE|{SelfAddress}").ConfigureAwait(false);
    }

    private async Task HandleAnnounceAsync(string _newSpookAddress)
    {
        using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
        {
            if (!HealthyTangleSpooks.Contains(_newSpookAddress))
            {
                HealthyTangleSpooks.Add(_newSpookAddress);
            }
        }

        await UpdatePrecedenceAsync().ConfigureAwait(false);
    }

    private void HandleData(string _data)
    {
        // placeholder
    }

    private async Task HandleHealthAsync(string _healthStatusJson)
    {
        HealthStatusJson? _healthStatus = HealthStatusJson.Parse(_healthStatusJson);

        if (_healthStatus == null)
        {
            return;
        }

        if (ExpectedTangleSpooks.Contains(_healthStatus.Address))
        {
            HealthUpdate? _previousHealthUpdate;

            using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
            {
                // if this tangle was certainly unhealthy before, add back to healthy
                //
                //  note that healthy doesn't mean entangled.
                //
                if (UnhealthyTangleSpooks.Contains(_healthStatus.Address))
                {
                    UnhealthyTangleSpooks.Remove(_healthStatus.Address);
                }

                // try to capture the last update, if there was one
                TangleHealthUpdates.TryGetValue(_healthStatus.Address, out _previousHealthUpdate);

                TangleHealthUpdates[_healthStatus.Address] = new HealthUpdate(
                    _healthStatus,
                    DateTime.UtcNow
                );
            }

            // if the precedence (configured or current) changed, double check precedence is correct.
            if (_previousHealthUpdate != null)
            {
                if (
                        _previousHealthUpdate.Status.CurrentPrecedence != _healthStatus.CurrentPrecedence
                    ||
                        _previousHealthUpdate.Status.ConfiguredPrecedence != _healthStatus.ConfiguredPrecedence
                )
                {
                    await UpdatePrecedenceAsync().ConfigureAwait(false);
                }
            }
        }
    }

    private async Task UpdatePrecedenceAsync()
    {
        // do nothing if we are in a peer tangle
        if (CurrentPrecedence == SpookPrecedence.Peer)
        {
            return;
        }

        //// collect the scores for all spooks that are healthy in our tangle
        //
        // collects the statuses into a single list
        List<HealthStatusJson> _tangledSpookStatuses;
        using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
        {
            _tangledSpookStatuses = [..
                (
                    from
                        _item in TangleHealthUpdates.Values
                    select
                        _item.Status
                ),
                BuildSelfHealthStatusNoLock()
            ];
        }
        //
        // scores each spook based on it's configured precedence
        Dictionary<string, int> _entangledSpookScores = [];
        int _spookCount = 0;
        int _topScore = -10;
        foreach (var _deltaStatus in _tangledSpookStatuses)
        {
            // only score spooks that are entangled
            if (_deltaStatus.CurrentTangledState == SpookTangledState.Entangled)
            {
                // increment how many spooks are entangled
                _spookCount += 1;

                //// gather scores
                //
                int _score = (int)_deltaStatus.ConfiguredPrecedence;
                //
                if (_score > _topScore)
                {
                    _topScore = _score;
                }
                //
                _entangledSpookScores.Add(_deltaStatus.Address, _score);
                //
                ////
            }
        }
        ////// verifies we have a quorum
        ////
        if (_spookCount == 1 || (((double)_spookCount) / ((double)ExpectedTangleSpooks.Count) <= 0.5d))
        {
            // if there is only one spook in our tangle, or less or equal to half reachable, we assume the other spooks are handling things.
            todo("we have to dim until we have a quorum");
        }
        ////
        //////
        //
        // collects the list of addresses for the top scorers
        //  (typically one, but could temporarily be multiple)
        //
        List<string> _topScorers = [..
            (
                from
                    _item in _entangledSpookScores
                where
                    _item.Value == _topScore
                select
                    _item.Key
            )
        ];
        //
        // basic check if we are alone
        if (_entangledSpookScores.Count < 2 || _topScorers.Count == 0)
        {
            //
            // for clarity: A spook that is alone cannot form a quorum so it
            //  always assumes the rest of the tangle has taken over.
            //
            // this is why we mark as drifting and wait for reconnection.
            //


            // if we knew we were alone already, do nothing
            if (CurrentTangledState == SpookTangledState.Drifting)
            {
                return;
            }

            CurrentTangledState = SpookTangledState.Drifting;
            todo("whatever should be done once we are alone");
        }
        //
        ////


        //// now that we have scores, we will try to make sure the top spook is selected
        //
        // if the top spook is already the top, we don't need to do anything
        using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
        {
            int _topPrecendenceCount = 0;
            foreach (var _deltaScorer in _topScorers)
            {
                if (TangleHealthUpdates[_deltaScorer].Status.CurrentPrecedence == SpookPrecedence.Top)
                {
                    _topPrecendenceCount += 1;

                    // once we have more than one, we exit the loop as that is all we need to know.
                    if (_topPrecendenceCount > 1)
                    {
                        break;
                    }
                }
            }

            // the top spook is already the correct one so we can exit
            if (_topPrecendenceCount == 1)
            {
                return;
            }
        }
        //
        // at this point, 0 or 2+ spooks think they are the top...
        //
        //  ultimately, the top configured spook isn't the top one at this point.
        //
        if (_topScorers.Count == 1)
        {

            // at this point there is a spook that should be the top but it isn't, so we need to vote it in
            todo("vote top scorer");
        }
        else
        {

            // at this point there is a spook that should be the top but it isn't, so we need to vote it in
            todo("vote top scorer");
        }
        //
        ////
    }

    private async Task StartHealthUpdatesAsync()
    {
        while (!StopSpook.Task.IsCompleted)
        {
            // capture start ticks so we can deduct the duration of processing from the wait
            long _startTicks = DateTime.Now.Ticks;


            // update the list of healthy and unhealthy spooks based on our last received health from each
            DateTimeOffset now = DateTime.UtcNow;
            using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
            {
                List<string> _unhealthySpooks;

                _unhealthySpooks = [..
                    (
                        from
                            _item in TangleHealthUpdates
                        where
                            _item.Key != SelfAddress && ((now - _item.Value.LastHealthUpdateReceived) > HealthTimeout)
                        select
                            _item.Key
                    )
                ];
                foreach (var spook in _unhealthySpooks)
                {
                    HealthyTangleSpooks.Remove(spook);
                    TangleHealthUpdates.Remove(spook);

                    UnhealthyTangleSpooks.Add(spook);
                }
            }

            ///////////////////////////////////////////////
            // this section is about performing updates based on health changes?
            ///////////////////////////////////////////////

            await UpdatePrecedenceAsync().ConfigureAwait(false);

            // Update state
            todo("this is wrong, need a more rigorous mechanism for this lol");
            CurrentTangledState = HealthyTangleSpooks.Count > 1 ? SpookTangledState.Entangled : SpookTangledState.Alone;

            ///////////////////////////////////////////////


            // Send health update
            string _selfHealth = (await BuildSelfHealthStatusAsync().ConfigureAwait(false)).ToJsonString() ?? ""; ;
            await BroadcastAsync($"HEALTH|{_selfHealth}").ConfigureAwait(false);


            // lastly we clean up message task list    
            using (await MessageProcessingLock.LockAsync().ConfigureAwait(false))
            {
                MessageProcessing.RemoveAll(_item => _item.IsCompleted || _item.IsCanceled);
            }


            // wait for the health interval minus how long ^ this took to run
            long _durationTicks = DateTime.Now.Ticks - _startTicks;
            if (_durationTicks > 0)
            {
                // wait the duration or when this spook shuts down
                await Task.WhenAny(Task.Delay(HealthInterval.Add(new TimeSpan(-_durationTicks))), StopSpook.Task).ConfigureAwait(false);
            }
        }

        // make sure all our messages are done processing before we dim
        await Task.WhenAll(MessageProcessing);

        // let tangle know we are going down
        await BroadcastAsync($"DIMMING|{SelfAddress}").ConfigureAwait(false);
    }

    protected async Task<HealthStatusJson> BuildSelfHealthStatusAsync()
    {
        using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
        {
            return BuildSelfHealthStatusNoLock();
        }
    }

    protected HealthStatusJson BuildSelfHealthStatusNoLock()
    {
        return new HealthStatusJson
        {
            Address = SelfAddress,
            ConfiguredPrecedence = ConfiguredPrecedence,
            CurrentPrecedence = CurrentPrecedence,
            CurrentTangledState = CurrentTangledState,
            TangleHealth = [..
                (
                    from
                        _item in TangleHealthUpdates
                    select
                        _item.Value.Status
                    into _itemWithoutDeeperHealth
                    let _ = _itemWithoutDeeperHealth.TangleHealth = null
                    select _itemWithoutDeeperHealth
                )
            ]
        };
    }
}