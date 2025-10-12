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
///     - Top + (0+) TopPeer + (0+) BottomPeer + (0+) Bottom
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

    protected readonly AsyncLock CurrentTangledStateLock;

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
    /// Be aware the health loop cleans this up* and awaits these before shutting down.
    /// 
    /// *only completed/cancelled tasks are cleaned up as expected.
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
        foreach (var deltaSpook in _expectedTangleSpooks)
        {
            ExpectedTangleSpooks.Add(deltaSpook.ToLower());
        }
        SelfAddress = _selfAddress;
        HealthInterval = TimeSpan.FromSeconds(_healthIntervalSeconds);
        HealthTimeout = TimeSpan.FromSeconds(_healthTimeoutSeconds);

        StopSpook = new();
        CurrentTangledState = SpookTangledState.Dim;
        CurrentTangledStateLock = AsyncLock.Create();
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
    public async Task ReceiveDataToDistributeAsync(string _data)
    {
        await BroadcastAsync($"DATA|{_data}").ConfigureAwait(false);
    }

    private void JoinTangle(string[] _tangleAddresses)
    {
        foreach (var deltaAddress in _tangleAddresses)
        {
            SendMessage(deltaAddress, $"JOIN|{SelfAddress}");
        }
    }

    private async Task BroadcastAsync(string _message)
    {
        List<string> cloneHealthyTangleSpooks = [];
        using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
        {
            cloneHealthyTangleSpooks.AddRange(HealthyTangleSpooks);
        }

        foreach (var spook in cloneHealthyTangleSpooks)
        {
            if (spook != SelfAddress)
            {
                SendMessage(spook, _message);
            }
        }
    }

    private void SendMessage(string _targetAddress, string _message)
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
        var parts = _message.Split('|', 2);
        if (parts.Length < 2) return;
        string type = parts[0];
        string payload = parts[1];

        Task process;

        switch (type)
        {
            // this is called by new spooks joining the tangle
            //  (they are not done joining until the announce)
            case "JOIN":
                process = Task.Run(() => HandleJoinAsync(_fromAddress));
                break;

            // this is the reply to joiners, with health status
            case "WELCOME":
                process = Task.Run(() => HandleWelcomeAsync(payload));
                break;

            // this is called by a spook that has completed joining
            case "ANNOUNCE":
                process = Task.Run(() => HandleAnnounceAsync(payload));
                break;

            // distribution of data payload
            case "DATA":
                process = Task.Run(() => HandleDataAsync(payload));
                break;

            // if a single integration fails while the top spook is healthy it
            //  will try to offload that process to another spook (1 spook).
            case "OFFLOAD":
                process = Task.Run(() => HandleOffloadAsync(payload));
                break;

            // heartbeat to let the other spooks know everything is good
            case "HEALTH":
                process = Task.Run(() => HandleHealthAsync(payload));
                break;

            // this is called when a spook notices something wrong with the
            //  top spook - or by top spook when going down expectedly
            case "BALLOT":
                process = Task.Run(() => HandleBallotAsync(payload));
                break;

            // called when a spook receives ballots from all healthy spooks in
            //  the tangle, on their consensus
            case "TUNE":
                process = Task.Run(() => HandleTuneAsync(payload));
                break;

            // called when a spook shuts down expectedly
            //  (regardless of it's tangled state)
            case "DIMMING":
                process = Task.Run(() => HandleDimmingAsync(payload));
                break;

            default:
                await LogAsync($"Receieved unexpected payload type '{type}' - ignoring.").ConfigureAwait(false);
                return;
        }

        using (await MessageProcessingLock.LockAsync().ConfigureAwait(false))
        {
            MessageProcessing.Add(process);
        }
    }

    private async Task HandleJoinAsync(string _newSpookAddress)
    {
        if (!ExpectedTangleSpooks.Contains(_newSpookAddress.ToLower()))
        {
            return;
        }

        string welcomePayload = (await BuildSelfHealthStatusAsync().ConfigureAwait(false)).ToJsonString() ?? "";
        SendMessage(_newSpookAddress, $"WELCOME|{welcomePayload}");
    }

    private async Task HandleWelcomeAsync(string _payload)
    {
        HealthStatusJson? theirHealthStatus = HealthStatusJson.Parse(_payload);

        if (theirHealthStatus == null || theirHealthStatus.TangleHealth == null)
        {
            return;
        }

        using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
        {
            foreach (var deltaStatus in theirHealthStatus.TangleHealth)
            {
                if (deltaStatus.CurrentTangledState == SpookTangledState.Entangled)
                {
                    if (!HealthyTangleSpooks.Contains(deltaStatus.Address))
                    {
                        HealthyTangleSpooks.Add(deltaStatus.Address);
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

    private async Task HandleHealthAsync(string _healthStatusJson)
    {
        HealthStatusJson? healthStatus = HealthStatusJson.Parse(_healthStatusJson);

        if (healthStatus == null)
        {
            return;
        }

        if (ExpectedTangleSpooks.Contains(healthStatus.Address))
        {
            // if someone in our tangle sent a health update


            HealthUpdate? previousHealthUpdate = null;

            using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
            {
                // if this tangle was certainly unhealthy before, add back to healthy (by removing from unhealthy)
                //
                //  note that healthy doesn't mean entangled.
                //
                if (UnhealthyTangleSpooks.Contains(healthStatus.Address))
                {
                    UnhealthyTangleSpooks.Remove(healthStatus.Address);
                }

                // try to capture the last update, if there was one
                TangleHealthUpdates.TryGetValue(healthStatus.Address, out previousHealthUpdate);

                TangleHealthUpdates[healthStatus.Address] = new HealthUpdate(
                    healthStatus,
                    DateTime.UtcNow
                );
            }

            // if the precedence (configured or current) changed, double check precedence is correct.
            if (previousHealthUpdate != null)
            {
                if (
                        previousHealthUpdate.Status.CurrentPrecedence != healthStatus.CurrentPrecedence
                    ||
                        previousHealthUpdate.Status.ConfiguredPrecedence != healthStatus.ConfiguredPrecedence
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
        //
        //  Note: If all nodes are peers, no list of who is the top/bottom matters.
        //        This is also a method to run a single node in an emergency.
        //
        if (CurrentPrecedence == SpookPrecedence.Peer)
        {
            return;
        }

        //// collect the scores for all spooks that are healthy in our tangle
        //
        // collects the statuses into a single list
        List<HealthStatusJson> tangledSpookStatuses;
        using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
        {
            tangledSpookStatuses = [..
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
        Dictionary<string, int> entangledSpookScores = [];
        int spookCount = 0;
        int topScore = -10;
        foreach (var deltaStatus in tangledSpookStatuses)
        {
            // only score spooks that are entangled
            if (deltaStatus.CurrentTangledState == SpookTangledState.Entangled)
            {
                // increment how many spooks are entangled
                spookCount += 1;

                //// gather scores
                //
                int score = (int)deltaStatus.ConfiguredPrecedence;
                //
                if (score > topScore)
                {
                    topScore = score;
                }
                //
                entangledSpookScores.Add(deltaStatus.Address, score);
                //
                ////
            }
        }
        ////// verifies we have a quorum
        ////
        if (spookCount == 1 || (((double)spookCount) / ((double)ExpectedTangleSpooks.Count) <= 0.5d))
        {
            // if there is only one spook in our tangle, or less or equal to half reachable, we assume the other spooks are handling things.
            await UpdateSpookTangleStateAsync(SpookTangledState.Dim, $"Cannot form a quorum with {spookCount} of {ExpectedTangleSpooks.Count} spooks healthy.");

            return;
        }
        ////
        //////
        //
        // collects the list of addresses for the top scorers
        //  (typically one, but could temporarily be multiple)
        //
        List<string> topScorers = [..
            (
                from
                    _item in entangledSpookScores
                where
                    _item.Value == topScore
                select
                    _item.Key
            )
        ];
        //
        // basic check if we are alone
        if (entangledSpookScores.Count < 2 || topScorers.Count == 0)
        {
            //
            // for clarity: A spook that is alone cannot form a quorum so it
            //  always assumes the rest of the tangle has taken over.
            //
            // this is why we mark as drifting and wait for reconnection.
            //


            // if we are already not entangled we can exit here
            if (CurrentTangledState != SpookTangledState.Entangled)
            {
                return;
            }

            // this call handles the steps to change into drifting mode
            await UpdateSpookTangleStateAsync(SpookTangledState.Drifting, "Spook is alone and unable to form a quorum.");

            // nothing else to do here
            return;
        }
        //
        ////


        //// now that we have scores, we will try to make sure the top spook is selected
        //
        // if the top spook is already the top, we don't need to do anything
        using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
        {
            int topPrecendenceCount = 0;
            foreach (var deltaScorer in topScorers)
            {
                if (TangleHealthUpdates[deltaScorer].Status.CurrentPrecedence == SpookPrecedence.Top)
                {
                    topPrecendenceCount += 1;

                    // if we collect more than one, we exit the loop as that is all we need to know.
                    if (topPrecendenceCount > 1)
                    {
                        break;
                    }
                }
            }

            // the top spook is already the correct one so we can exit
            if (topPrecendenceCount == 1)
            {
                return;
            }
        }
        //
        // at this point, 0 or 2+ spooks think they are the top...
        //
        //  ultimately, the top configured spook isn't the top one at this point.
        //
        // at this point there is a spook that should be the top but it isn't, so we need to vote it in
        if (topScorers.Count == 1)
        {
            // we know who should be the top since there's only one

            await BroadcastAsync($"BALLOT|${SelfAddress},vote,${topScorers[0]}");
        }
        else
        {
            // we know a group where one should be the top

            string? topAddress;
            using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
            {
                topAddress = (
                    from
                        _item in TangleHealthUpdates
                    where
                        topScorers.Contains(_item.Key)
                    group
                        _item by _item.Key into _grouped
                    select (
                        from
                            _groupedItem in _grouped
                        orderby _groupedItem.Value.LastHealthUpdateReceived descending
                        select _groupedItem.Key
                    ).FirstOrDefault()
                ).FirstOrDefault();
            }

            if(topAddress == null)
            {
                // we have no top scorers anymore, this is an edge-case race condition and possible
                //
                //  for now, we will exit out
                return;
            }
            else
            {
                // vote for who we received the most recent health update from
                await BroadcastAsync($"BALLOT|${SelfAddress},vote,${topAddress}");
            }
        }
        //
        ////
    }

    private async Task StartHealthUpdatesAsync()
    {
        while (!StopSpook.Task.IsCompleted)
        {
            // capture start ticks so we can deduct the duration of processing from the wait
            long startTicks = DateTime.Now.Ticks;


            // update the list of healthy and unhealthy spooks based on our last received health from each
            DateTimeOffset now = DateTime.UtcNow;
            using (await HealthyTangleSpookLock.LockAsync().ConfigureAwait(false))
            {
                List<string> unhealthySpooks;

                unhealthySpooks = [..
                    (
                        from
                            _item in TangleHealthUpdates
                        where
                            _item.Key != SelfAddress && ((now - _item.Value.LastHealthUpdateReceived) > HealthTimeout)
                        select
                            _item.Key
                    )
                ];
                foreach (var spook in unhealthySpooks)
                {
                    HealthyTangleSpooks.Remove(spook);
                    TangleHealthUpdates.Remove(spook);

                    UnhealthyTangleSpooks.Add(spook);
                }
            }


            // make sure the precedece is still calculated, based on potential health changes
            await UpdatePrecedenceAsync().ConfigureAwait(false);


            // Send health update
            string selfHealth = (await BuildSelfHealthStatusAsync().ConfigureAwait(false)).ToJsonString() ?? ""; ;
            await BroadcastAsync($"HEALTH|{selfHealth}").ConfigureAwait(false);


            // lastly we clean up message task list    
            using (await MessageProcessingLock.LockAsync().ConfigureAwait(false))
            {
                MessageProcessing.RemoveAll(_item => _item.IsCompleted || _item.IsCanceled);
            }


            // calculate the health interval, minus how long all ^ this took to run
            TimeSpan waitDuration = HealthInterval.Add(new TimeSpan(-(DateTime.Now.Ticks - startTicks)));
            if (waitDuration > TimeSpan.Zero)
            {
                // wait for the health interval minus the duration, or when this spook shuts down
                await Task.WhenAny(Task.Delay(waitDuration), StopSpook.Task).ConfigureAwait(false);
            }
        }

        using (await MessageProcessingLock.LockAsync().ConfigureAwait(false))
        {
            // make sure all our messages are done processing before we dim
            await Task.WhenAll(MessageProcessing);
        }

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_state"></param>
    /// <param name="_verbalReason"></param>
    /// <returns></returns>
    /// <remarks>
    /// possible transitions:
    ///
    ///      Successful starting up == Dim -> Snapping
    ///                Synchronized == Snapping -> Entangled
    ///
    ///    Shutting down expectedly == Entangled -> Dim
    ///
    ///  Failure within tangle/self == Entangled -> Drifting
    ///
    ///                    Recovery == Drifting -> Snapping
    ///
    ///    Shut down during failure == Drifting -> Dim
    ///       Shut down during sync == Snapping -> Dim
    ///
    ///           Issue starting up == Dim -> Drifting
    ///           
    ///           Issue during sync == Snapping -> Drifting
    /// 
    /// Not possible:
    ///     Dim -> Entangled
    ///     Dim -> Dim
    ///     
    ///     Entangled -> Snapping
    ///     Entangled -> Entangled
    ///     
    ///     Drifting -> Entangled
    ///     Drifting -> Drifting
    ///     
    ///     Snapping -> Snapping
    ///</remarks>
    protected async Task UpdateSpookTangleStateAsync(SpookTangledState _state, string _verbalReason)
    {
        using(await CurrentTangledStateLock.LockAsync().ConfigureAwait(false))
        {
            await LogAsync();

            // don't try to chance states if we already are in requested state
            if(CurrentTangledState == _state)
            {
                return;
            }

            switch(CurrentTangledState)
            {
                // was in a good state
                case SpookTangledState.Entangled:
                    switch (_state)
                    {
                        // failure within tangle/self
                        case SpookTangledState.Drifting:
                            todo();

                            CurrentTangledState = SpookTangledState.Drifting;
                            return;

                        // expected shut down call
                        case SpookTangledState.Dim:
                            todo();

                            CurrentTangledState = SpookTangledState.Dim;
                            return;
                    }
                    break;

                // was offline
                case SpookTangledState.Dim:
                    switch (_state)
                    {
                        // successful starting up
                        case SpookTangledState.Snapping:
                            todo();

                            CurrentTangledState = SpookTangledState.Snapping;
                            return;

                        // issue starting up
                        case SpookTangledState.Drifting:
                            todo();

                            CurrentTangledState = SpookTangledState.Drifting;
                            return;
                    }
                    break;

                // was synchronizing with tangle
                case SpookTangledState.Snapping:
                    switch (_state)
                    {
                        // synchronized
                        case SpookTangledState.Entangled:
                            todo();

                            CurrentTangledState = SpookTangledState.Entangled;
                            return;

                        // expected shut down call during sync
                        case SpookTangledState.Dim:

                            CurrentTangledState = SpookTangledState.Dim;
                            todo();
                            return;

                        // unexpected issue during sync
                        case SpookTangledState.Drifting:
                            todo();

                            CurrentTangledState = SpookTangledState.Drifting;
                            return;
                    }
                    break;

                // was out of sync with tangle
                case SpookTangledState.Drifting:
                    switch (_state)
                    {
                        // recovering from a failure
                        case SpookTangledState.Snapping:
                            todo();

                            CurrentTangledState = SpookTangledState.Snapping;
                            return;

                        // expected shut down call during failure
                        case SpookTangledState.Dim:
                            todo();

                            CurrentTangledState = SpookTangledState.Dim;
                            return;
                    }
                    break;
            }
        }

        // at this point, an invalid state change tried to occur            
        throw new Exception(
            string.Format(
                "Invalid state change from {0} -> {1}",
                CurrentTangledState,
                _state
            )
        );
    }

    /// <summary>
    /// Distribution of a data payload from another Spook.
    /// </summary>
    private async Task HandleDataAsync(string _data)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Special case where top spook has an integration that fails and they offload it onto us.
    /// </summary>
    public async Task HandleOffloadAsync(string _payload)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Called when another spook noticed top spook issues (or by top spook going down).
    /// </summary>
    public async Task HandleBallotAsync(string _payload)
    {
        // 0 = address
        // 1 = task
        // 2 = top voted
        string[] voteOptions = _payload.Split(',');

        throw new NotImplementedException();
    }

    /// <summary>
    /// Receiving tally from another spook on ballots for the next top spook.
    /// </summary>
    public async Task HandleTuneAsync(string _payload)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Called by a spook that is about to shut down.
    /// </summary>
    public async Task HandleDimmingAsync(string _payload)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Method to silence all my todo markers. Rename to see where work needs to be done
    /// </summary>
    /// <param name="_test"></param>
    protected static void todo0(string? _test = null)
    {
        Console.WriteLine(_test);
    }
}