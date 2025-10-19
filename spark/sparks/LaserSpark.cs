using luminary.mapping;
using luminary.util;

namespace luminary.spark;

public class LaserSpark : Spark
{
    public LaserSpark(MappingFlow _flow, Prism _input) : base(_flow, _input)
    {
    }

    protected override Task<Panicable<Prism?>> TriggerDefinitionAsync()
    {
        throw new NotImplementedException();
    }
}