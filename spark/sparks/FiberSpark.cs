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
    /// <summary>
    /// The key for the mapping flow type (luminary.mapping.FlowType).
    /// 
    /// This is the mapping flow call that must be called to process the input for the
    ///     payload that the http caller expects.
    /// </summary>
    public const string FLOW_TYPE_KEY = "mapping-flow-type";

    /// <summary>
    /// The key for the mapping flow parameters (list of strings).
    /// 
    /// This is the mapping flow call that must be called to process the input for the
    ///     payload that the http caller expects.
    /// </summary>
    public const string FLOW_PARAMETERS_KEY = "mapping-flow-parameters";


    protected string? FiberFlowType;

    protected List<string>? FiberFlowParameters;

    public FiberSpark(MappingFlow _flow, Prism _input, PrismOperator _sparkConfiguration) : base(_flow, _input, _sparkConfiguration)
    {
    }

    public override void ValidateConfiguration()
    {
        using (var el = new ExceptionList().ThrowIfExceptions())
        {
            Dictionary<string, OperatorValue>? config = SparkConfiguration.GetValue();


            if (config == null)
            {
                // config is null
                el.AddExceptionString($"Cannot create fiber spark, configuration is null, must have keys: '{FLOW_TYPE_KEY}', '{FLOW_PARAMETERS_KEY}'.");
            }
            else
            {
                // verify the destination flow type is good
                if (!config.TryGetValue(FLOW_TYPE_KEY, out OperatorValue? destinationFlowType))
                {
                    el.AddExceptionString($"Cannot create fiber spark, configuration missing key '{FLOW_TYPE_KEY}'.");
                }
                else if (destinationFlowType == null || destinationFlowType is not StringOperator)
                {
                    el.AddExceptionString($"Cannot create fiber spark, configuration key '{FLOW_TYPE_KEY}' was null or not a string.");
                }
                else
                {
                    FiberFlowType = ((StringOperator)destinationFlowType).GetValue();
                }

                // verify the destination flow parameters are good
                if (!config.TryGetValue(FLOW_PARAMETERS_KEY, out OperatorValue? destinationFlowParameters))
                {
                    el.AddExceptionString($"Cannot create fiber spark, configuration missing key '{FLOW_PARAMETERS_KEY}'.");
                }
                else if (destinationFlowParameters == null || destinationFlowParameters is not ArrayOperator || ((ArrayOperator)destinationFlowParameters).ArrayType != OperatorValue.OperatorValueType.String)
                {
                    el.AddExceptionString($"Cannot create fiber spark, configuration key '{FLOW_PARAMETERS_KEY}' was null or not an array of strings.");
                }
                else
                {
                    FiberFlowParameters = ((ArrayOperator)destinationFlowParameters).GetValue()?.Select<OperatorValue, string?>(_deltaItem => ((StringOperator)_deltaItem).GetValue()).OfType<string>().ToList();
                }
            }
        }
    }

    protected override async Task<Panicable<Prism?>> TriggerDefinitionAsync()
    {
        Panicable<Prism?> output = new();

        try
        {
            // call the mapper (maps the event payload to the expected prism for the caller)
            output.ReturnValue = MappingFlow.ProcessFlow(
                Flow,
                FiberFlowType ?? throw new Exception()
            )(
                FlowInput,
                FiberFlowParameters ?? throw new Exception()
            );
        }
        catch (Exception e)
        {
            output.ActivatePanic(e);
        }

        return output;
    }
}
