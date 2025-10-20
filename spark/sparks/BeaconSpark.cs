using luminary.flow;
using luminary.mapping;
using luminary.mapping.functions;
using luminary.util;

namespace luminary.spark;

/// <summary>
/// A beacon spark is a task that is created every time a timer event is triggered.
/// 
/// The beacon is configured to forward a payload onto an endpoint.
/// </summary>
public class BeaconSpark : Spark
{
    /// <summary>
    /// The key for the destination url (where to send payload).
    /// </summary>
    public const string DESTINATION_URL_KEY = "destination-url";

    /// <summary>
    /// The key for the destination mapping flow type (luminary.mapping.FlowType).
    /// 
    /// This is the mapping flow call that must be called to process the input for the
    ///     payload that the destination expects.
    /// </summary>
    public const string DESTINATION_FLOW_TYPE_KEY = "destination-mapping-flow-type";

    /// <summary>
    /// The key for the destination mapping flow parameters (list of strings).
    /// 
    /// This is the mapping flow call that must be called to process the input for the
    ///     payload that the destination expects.
    /// </summary>
    public const string DESTINATION_FLOW_PARAMETERS_KEY = "destination-mapping-flow-parameters";



    /// <summary>
    /// The key for the callback url (where to send the reply).
    /// </summary>
    public const string CALLBACK_URL_KEY = "callback-url";

    /// <summary>
    /// The key for the callback mapping flow type (luminary.mapping.FlowType).
    /// 
    /// This is the mapping flow call that must be called to process the reply
    ///     from the destination and who's output goes to the callback.
    /// </summary>
    public const string CALLBACK_MAPPING_FLOW_TYPE_KEY = "callback-mapping-flow-type";

    /// <summary>
    /// The key for the callback mapping flow parameters (list of strings).
    /// 
    /// This is the mapping flow call that must be called to process the reply
    ///     from the destination and who's output goes to the callback.
    /// </summary>
    public const string CALLBACK_MAPPING_FLOW_PARAMETERS_KEY = "callback-mapping-flow-parameters";



    protected string? DestinationUrl;

    protected string? DestinationFlowType;

    protected List<string>? DestinationFlowParameters;

    protected string? CallbackUrl;

    protected string? CallbackFlowType;

    protected List<string>? CallbackFlowParameters;

    public BeaconSpark(MappingFlow _flow, Prism _input, PrismOperator _sparkConfiguration) : base(_flow, _input, _sparkConfiguration)
    {
        DestinationUrl = null;
        DestinationFlowType = null;
        DestinationFlowParameters = null;

        CallbackUrl = null;
        CallbackFlowType = null;
        CallbackFlowParameters = null;
    }

    public override void ValidateConfiguration()
    {
        using (var el = new ExceptionList().ThrowIfExceptions())
        {
            Dictionary<string, OperatorValue>? config = SparkConfiguration.GetValue();

            if (config == null)
            {
                // config is null
                el.AddExceptionString($"Cannot create beacon spark, configuration is null, must have keys: '{DESTINATION_URL_KEY}', '{DESTINATION_FLOW_TYPE_KEY}', '{DESTINATION_FLOW_PARAMETERS_KEY}', '{CALLBACK_URL_KEY}', '{CALLBACK_MAPPING_FLOW_TYPE_KEY}' and '{CALLBACK_MAPPING_FLOW_PARAMETERS_KEY}'.");
            }
            else
            {
                // verify the destination url is good
                if (!config.TryGetValue(DESTINATION_URL_KEY, out OperatorValue? destinationUrl))
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration missing key '{DESTINATION_URL_KEY}'.");
                }
                else if (destinationUrl == null || destinationUrl is not StringOperator)
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration key '{DESTINATION_URL_KEY}' was null or not a string.");
                }
                else
                {
                    DestinationUrl = ((StringOperator)destinationUrl).GetValue();
                }

                // verify the destination flow type is good
                if (!config.TryGetValue(DESTINATION_FLOW_TYPE_KEY, out OperatorValue? destinationFlowType))
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration missing key '{DESTINATION_FLOW_TYPE_KEY}'.");
                }
                else if (destinationFlowType == null || destinationFlowType is not StringOperator)
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration key '{DESTINATION_FLOW_TYPE_KEY}' was null or not a string.");
                }
                else
                {
                    DestinationFlowType = ((StringOperator)destinationFlowType).GetValue();
                }

                // verify the destination flow parameters are good
                if (!config.TryGetValue(DESTINATION_FLOW_PARAMETERS_KEY, out OperatorValue? destinationFlowParameters))
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration missing key '{DESTINATION_FLOW_PARAMETERS_KEY}'.");
                }
                else if (destinationFlowParameters == null || destinationFlowParameters is not ArrayOperator || ((ArrayOperator)destinationFlowParameters).ArrayType != OperatorValue.OperatorValueType.String)
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration key '{DESTINATION_FLOW_PARAMETERS_KEY}' was null or not an array of strings.");
                }
                else
                {
                    DestinationFlowParameters = ((ArrayOperator)destinationFlowParameters).GetValue()?.Select<OperatorValue, string?>(_deltaItem => ((StringOperator)_deltaItem).GetValue()).OfType<string>().ToList();
                }




                // verify the callback url is good
                if (!config.TryGetValue(CALLBACK_URL_KEY, out OperatorValue? callbackUrl))
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration missing key '{CALLBACK_URL_KEY}'.");
                }
                else if (callbackUrl == null || callbackUrl is not StringOperator)
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration key '{CALLBACK_URL_KEY}' was null or not a string.");
                }
                else
                {
                    CallbackUrl = ((StringOperator)callbackUrl).GetValue();
                }

                // verify the callback flow type is good
                if (!config.TryGetValue(CALLBACK_MAPPING_FLOW_TYPE_KEY, out OperatorValue? callbackFlowType))
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration missing key '{CALLBACK_MAPPING_FLOW_TYPE_KEY}'.");
                }
                else if (callbackFlowType == null || callbackFlowType is not StringOperator)
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration key '{CALLBACK_MAPPING_FLOW_TYPE_KEY}' was null or not a string.");
                }
                else
                {
                    CallbackFlowType = ((StringOperator)callbackFlowType).GetValue();
                }

                // verify the callback flow parameters are good
                if (!config.TryGetValue(CALLBACK_MAPPING_FLOW_PARAMETERS_KEY, out OperatorValue? callbackFlowParameters))
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration missing key '{CALLBACK_MAPPING_FLOW_PARAMETERS_KEY}'.");
                }
                else if (callbackFlowParameters == null || callbackFlowParameters is not ArrayOperator || ((ArrayOperator)callbackFlowParameters).ArrayType != OperatorValue.OperatorValueType.String)
                {
                    el.AddExceptionString($"Cannot create beacon spark, configuration key '{CALLBACK_MAPPING_FLOW_PARAMETERS_KEY}' was null or not an array of strings.");
                }
                else
                {
                    CallbackFlowParameters = ((ArrayOperator)callbackFlowParameters).GetValue()?.Select<OperatorValue, string?>(_deltaItem => ((StringOperator)_deltaItem).GetValue()).OfType<string>().ToList();
                }
            }
        }
    }

    /// <summary>
    /// This is a bit complicated, but here's what this does:
    /// 
    ///     1. Accept the input from the event trigger (FlowInput).
    /// 
    ///     2. Process it thru the "destination" mapper as configured.
    /// 
    ///     3. Take the output of #2 and send it to the "destination" url.
    /// 
    ///     4. Take the output of #3 and process it thru the "callback" mapper.
    /// 
    ///     5. Take the output of #4 and send it to the "callback" url.
    /// 
    ///     6. Return the output of #5.
    /// 
    /// This will panic if any of the payloads (besides #6) are null or other basic issue happens.
    /// </summary>
    /// <returns>The output as expected, see summary. May panic if an issue arises.</returns>
    protected override async Task<Panicable<Prism?>> TriggerDefinitionAsync()
    {
        Panicable<Prism?> output = new();

        try
        {
            // call the destination mapper (maps the event payload to the expected prism for the destination)
            Prism prismToSendToDestination = MappingFlow.ProcessFlow(
                Flow,
                DestinationFlowType ?? throw new Exception()
            )(
                FlowInput,
                DestinationFlowParameters ?? throw new Exception()
            ) ?? throw new Exception("Destination payload mapping failed as the resulting Prism is unexectedly null.");

            // collect the http reply/response from the destination by calling the destination url while passing mapper output
            Prism payloadToMapForCallback = await todo("send to destination url", DestinationUrl, prismToSendToDestination).ConfigureAwait(false) ?? throw new Exception("The response to the url call to the destination url is unexectedly null.");

            // call the callback mapper using the above http response as the input and collecting the reply/response
            Prism prismToSendToCallback = MappingFlow.ProcessFlow(
                Flow,
                CallbackFlowType ?? throw new Exception()
            )(
                payloadToMapForCallback,
                CallbackFlowParameters ?? throw new Exception()
            ) ?? throw new Exception("Callback payload mapping failed as the resulting Prism is unexectedly null.");

            // return the output of the http call to the callback url
            output.ReturnValue = await todo("send to callback url", CallbackUrl, prismToSendToCallback).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            output.ActivatePanic(e);
        }

        return output;
    }
}