using luminary.flow;
using luminary.mapping;
using luminary.mapping.functions;
using luminary.util;

namespace luminary.spark;

/// <summary>
/// This type of spark is triggered from an http listener.
/// 
/// The payload received flows into the mapper.
/// 
/// Once completed, the payload is returned.
/// </summary>
public class FiberSpark : Spark
{
    public FiberSpark(MappingFlow _flow, Prism _input, PrismOperator _sparkConfiguration) : base(_flow, _input, _sparkConfiguration)
    {
    }

    public override void ValidateConfiguration()
    {
        todo();
    }

    protected override Task<Panicable<Prism?>> TriggerDefinitionAsync()
    {
        todo();
    }
}
