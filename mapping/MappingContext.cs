using System.Threading;

namespace luminary.mapping;

public class MappingContext
{
    protected Dictionary<ulong, Prism> DataStore = [];

    protected ulong StepCounter = 0;

    public void Add(Prism data, ulong? step = null)
    {
        if(step == null)
        {
            step = Interlocked.Increment(ref StepCounter);
            
            // make sure we increment past any used keys
            while(DataStore.ContainsKey(StepCounter))
            {
                step = Interlocked.Increment(ref StepCounter);
            }
        }
        
        DataStore[step.Value] = data;
    }

    public Prism? Get(ulong step)
    {
        return DataStore.TryGetValue(step, out var _dataStore) ? _dataStore : null;
    }

    public Dictionary<ulong, Prism> GetAll()
    {
        return DataStore;
    }
}