namespace luminary.data.replication;

public class HealthUpdate(HealthStatusJson _status, DateTimeOffset _lastHealthUpdateReceived)
{
    public readonly HealthStatusJson Status = _status;

    public readonly DateTimeOffset LastHealthUpdateReceived = _lastHealthUpdateReceived;
}