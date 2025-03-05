using System.Threading;

namespace luminary.mapping;

/// <summary>
/// This is used to pass step outputs to future steps.
/// </summary>
public class MapperContext
{
    protected Dictionary<ulong, Prism> DataStore = [];

    protected ulong StepCounter = 0;

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

    public Prism? Get(ulong _step)
    {
        return DataStore.TryGetValue(_step, out var dataStore) ? dataStore : null;
    }

    public Dictionary<ulong, Prism> GetAll()
    {
        return DataStore;
    }
}