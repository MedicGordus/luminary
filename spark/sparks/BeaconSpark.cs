using luminary.mapping;
using luminary.util;

namespace luminary.spark;

public class BeaconSpark : Spark
{
    public BeaconSpark(MappingFlow _flow, Prism _input) : base(_flow, _input)
    {
    }

    protected override Task<Panicable<Prism?>> TriggerDefinitionAsync()
    {
        throw new NotImplementedException();
    }
}