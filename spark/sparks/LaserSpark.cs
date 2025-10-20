using luminary.flow;
using luminary.mapping;
using luminary.mapping.functions;
using luminary.util;

namespace luminary.spark;

/// <summary>
/// A laser spark is a task that is created every time a payload received event is triggered.
/// 
/// The laser is configured to buffer all unsent payloads and repeatedly try to deliver to
///     the configured endpoints until each receives the payload (and then the buffer can be
///     cleared).
/// </summary>
public class LaserSpark : Spark
{
    public LaserSpark(MappingFlow _flow, Prism _input, PrismOperator _sparkConfiguration) : base(_flow, _input, _sparkConfiguration)
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