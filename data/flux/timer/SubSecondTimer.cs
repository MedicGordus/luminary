namespace luminary.data.flux;

public class SubSecondTimer : FluxTimer
{
    public override Task WaitAsync(int _milliseconds)
    {
        return Task.Delay(_milliseconds);
    }
}