using System.Threading;

namespace luminary.mapping;

/// <summary>
/// This is used to pass step outputs to future steps.
/// </summary>
public class MapperContext
{
    public Dictionary<ulong, Prism> DataStore = [];

    protected ulong StepCounter = 0;

    /// <summary>
    /// Access to the root mapping flow which we need access to for the other mappers we might touch.
    /// 
    /// Typically method calls to flows use this.
    /// </summary>
    public MappingFlow RootFlow;

    public MapperContext(MappingFlow _rootFlow)
    {
        RootFlow = _rootFlow;
    }


    public void Add(Prism _data, ulong? _step = null)
    {
        if (_step == null)
        {
            _step = Interlocked.Increment(ref StepCounter);

            // make sure we increment past any used keys
            while (DataStore.ContainsKey(StepCounter))
            {
                _step = Interlocked.Increment(ref StepCounter);
            }
        }

        DataStore[_step.Value] = _data;
    }
}