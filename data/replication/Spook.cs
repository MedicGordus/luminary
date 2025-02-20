


namespace luminary.data.replication;

public enum SpookTangledState : int
{
    z_error = 0,

    /// <summary>
    /// In sync.
    /// </summary>
    Entangled = 1,

    /// <summary>
    /// Out of sync with Top Spook.
    /// </summary>
    Drifting = 2,

    /// <summary>
    /// Drifted, but getting back in sync with Top Spook.
    /// </summary>
    Snapping = 3,

    /// <summary>
    /// Starting up or intentionally shut down.
    /// </summary>
    Dead = 4
}

public enum SpookPrecedence : int
{
    z_error = 0,

    /// <summary>
    /// This is the primary Spook. Whenever the number of Top Spooks in a Tangle is not 1, all participants vote for the Top Spook.
    /// </summary>
    Top = 1,

    /// <summary>
    /// This is a peer spook. There cannot be Top or Bottom spooks in a Tangle with Peers.
    /// </summary>
    Peer = 2,

    /// <summary>
    /// This is a backup Spook, ready to vote for a new Top Spook if the Top Spook appears to go down.
    /// </summary>
    Bottom = 3
}

public class Spook
{
    protected SpookTangledState TangledState;

    protected SpookPrecedence Precedence;   
    
    protected readonly string SelfAddress;

    protected HashSet<string> KnownSpooks = new HashSet<string>();

    protected string TopSpookAddress;


    /// <summary>
    /// Lookup of each Tangled Spook's address and the last health check.
    /// </summary>
    protected Dictionary<string, DateTimeOffset> LastHealthUpdate = new Dictionary<string, DateTimeOffset>();

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

    public Spook(string _selfAddress, int _healthIntervalSeconds, int _healthTimeoutSeconds)
    {
        StopSpook = new();
        TangledState = SpookTangledState.Dead;
        HealthInterval = TimeSpan.FromSeconds(_healthIntervalSeconds);
        HealthTimeout = TimeSpan.FromSeconds(_healthTimeoutSeconds);

        SelfAddress = _selfAddress;
        TopSpookAddress = _selfAddress;
        KnownSpooks.Add(_selfAddress);
        LastHealthUpdate[_selfAddress] = DateTime.UtcNow;
    }

    public void Start(string[] _tangleAddresses)
    {
        StartListening();
        StartHealthUpdatesAsync();
        JoinTangle(_tangleAddresses);
    }

    // Called by external class to distribute data
    public void ReceiveDataToDistribute(string data)
    {
        Broadcast($"DATA|{data}");
    }

    private void JoinTangle(string[] _tangleAddresses)
    {
        foreach (var seed in _tangleAddresses)
        {
            SendMessage(seed, $"JOIN|{SelfAddress}");
        }
    }

    private void Broadcast(string message)
    {
        foreach (var spook in KnownSpooks)
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

    private void ProcessMessage(string _message, string _fromAddress)
    {
        var _parts = _message.Split('|', 2);
        if (_parts.Length < 2) return;
        string _type = _parts[0];
        string _payload = _parts[1];

        switch (_type)
        {
            case "JOIN":
                HandleJoin(_payload);
                break;
            case "WELCOME":
                HandleWelcome(_payload);
                break;
            case "ANNOUNCE":
                HandleAnnounce(_payload);
                break;
            case "DATA":
                HandleData(_payload);
                break;
            case "HEALTH":
                HandleHealth(_payload);
                break;
        }
    }

    private void HandleJoin(string _newSpookAddress)
    {
        if (!KnownSpooks.Contains(_newSpookAddress))
        {
            KnownSpooks.Add(_newSpookAddress);
            LastHealthUpdate[_newSpookAddress] = DateTime.UtcNow;
        }
        string _welcomePayload = $"{string.Join(",", KnownSpooks)}|{TopSpookAddress ?? ComputeTopSpook()}";
        SendMessage(_newSpookAddress, $"WELCOME|{_welcomePayload}");
    }

    private void HandleWelcome(string _payload)
    {
        var _parts = _payload.Split('|');
        if (_parts.Length < 2) return;
        string[] _spooks = _parts[0].Split(',');
        string _topSpook = _parts[1];

        foreach (var _spook in _spooks)
        {
            if (!KnownSpooks.Contains(_spook))
            {
                KnownSpooks.Add(_spook);
                LastHealthUpdate[_spook] = DateTime.UtcNow;
            }
        }
        if (string.IsNullOrEmpty(TopSpookAddress))
        {
            TopSpookAddress = _topSpook;
            UpdatePrecedence();
        }
        Broadcast($"ANNOUNCE|{SelfAddress}");
    }

    private void HandleAnnounce(string _newSpookAddress)
    {
        if (!KnownSpooks.Contains(_newSpookAddress))
        {
            KnownSpooks.Add(_newSpookAddress);
            LastHealthUpdate[_newSpookAddress] = DateTime.UtcNow;
        }
        TopSpookAddress = ComputeTopSpook();
        UpdatePrecedence();
    }

    private void HandleData(string _data)
    {
        // placeholder
    }

    private void HandleHealth(string _fromAddress)
    {
        if (KnownSpooks.Contains(_fromAddress))
        {
            LastHealthUpdate[_fromAddress] = DateTime.UtcNow;
        }
    }

    private string ComputeTopSpook()
    {
        return KnownSpooks.OrderBy(_item => _item).FirstOrDefault();
    }

    private void UpdatePrecedence()
    {
        todo("this is wrong");
        Precedence = (SelfAddress == TopSpookAddress) ? SpookPrecedence.Top : SpookPrecedence.Peer;
    }

    private async Task StartHealthUpdatesAsync()
    {
        while(!StopSpook.Task.IsCompleted)
        {
            // capture start ticks so we can deduct the duration of processing from the wait
            long _startTicks = DateTime.Now.Ticks;

            // Send health update
            Broadcast($"HEALTH|{SelfAddress}");

            // Check for timeouts
            DateTimeOffset now = DateTime.UtcNow;
            var toRemove = LastHealthUpdate
                .Where(kv => kv.Key != SelfAddress && (now - kv.Value) > HealthTimeout)
                .Select(kv => kv.Key)
                .ToList();
            foreach (var spook in toRemove)
            {
                KnownSpooks.Remove(spook);
                LastHealthUpdate.Remove(spook);
            }
            TopSpookAddress = ComputeTopSpook();
            UpdatePrecedence();

            // Update state
            TangledState = KnownSpooks.Count > 1 ? SpookTangledState.Entangled : SpookTangledState.Drifting;

            // wait for the health interval minus how long we took to run
            long _durationTicks = DateTime.Now.Ticks - _startTicks;
            await Task.WhenAny(Task.Delay(HealthInterval.Add(new TimeSpan(-_durationTicks))), StopSpook.Task);
        }
    }
}