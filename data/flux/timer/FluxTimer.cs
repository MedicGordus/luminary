namespace luminary.data.flux;

public abstract class FluxTimer : FluxEvent
{
    public abstract Task WaitAsync(int _milliseconds);
}