using luminary.data.storage;
using luminary.flow;
using luminary.mapping;
using luminary.mapping.functions;
using luminary.util;

namespace luminary.spark;

/// <summary>
/// The mushroom spark is unique in that it has two event triggers:
/// 
///     1. Receive a payload
/// 
///         When receiving a payload, the mushroom spark will buffer the payload into
///             now/later storage.
/// 
///     2. Receive request for data
/// 
///         Retrieves the payload from storage and forwards it to the callback url.
/// </summary>
public class MushroomSpark : Spark
{
    /// <summary>
    /// This is used to configure storage for this spark, setup via settings in the prismoperator.
    /// </summary>
    private readonly Mushroom Mushroom;

    public MushroomSpark(MappingFlow _flow, Prism _input, PrismOperator _sparkConfiguration) : base(_flow, _input, _sparkConfiguration)
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