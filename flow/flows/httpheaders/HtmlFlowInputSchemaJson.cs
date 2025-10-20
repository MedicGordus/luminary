using System.Text.Json;
using System.Text.Json.Serialization;
using luminary.mapping;
using luminary.mapping.functions;

namespace luminary.flow;

public class HtmlFlowInputSchemaJson
{
    /// <summary>
    /// The url to be called.
    /// </summary>
    [JsonPropertyName(URL_JSON_NAME)]
    public string? Url { get; set; }
    public const string URL_JSON_NAME = "url";

    /// <summary>
    /// The payload to send.
    /// </summary>
    [JsonPropertyName(PAYLOAD_JSON_NAME)]
    public string? JsonPayload { get; set; }
    public const string PAYLOAD_JSON_NAME = "payload";
    
    /// <summary>
    /// How many milliseconds to timeout (less than zero, zero or null to never timeout).
    /// </summary>
    [JsonPropertyName(MILLISECONDS_TO_TIMEOUT_JSON_NAME)]
    public int? MillisecondsToTimeout { get; set; }
    public const string MILLISECONDS_TO_TIMEOUT_JSON_NAME = "milliseconds-to-timeout";

    /// <summary>
    /// The http headers to send.
    /// </summary>
    [JsonPropertyName(HEADERS_JSON_NAME)]
    public List<HtmlFlowHeaderSchemaJson>? Headers { get; set; }
    public const string HEADERS_JSON_NAME = "headers";

    /// <summary>
    /// Helper function that converts prism to the expected object.
    /// </summary>
    public static HtmlFlowInputSchemaJson BuildFromPrism(Prism _input)
    {
        var basePrism = _input.Payload.GetValue() ?? throw new Exception(
            "Cannot build HtmlFlowInputSchemaJson from prism as the getvalue call returned null."
        );

        if (!basePrism.TryGetValue(URL_JSON_NAME, out var url) || url == null || url is not StringOperator urlString)
        {
            throw new Exception("Cannot build HtmlFlowInputSchemaJson from prism as the url was missing, null, or was not a string.");
        }
        if (!basePrism.TryGetValue(PAYLOAD_JSON_NAME, out var payload) || payload is not StringOperator payloadString)
        {
            throw new Exception("Cannot build HtmlFlowInputSchemaJson from prism as the payload was missing or was not a string.");
        }
        if (!basePrism.TryGetValue(MILLISECONDS_TO_TIMEOUT_JSON_NAME, out var millisecondsToTimeout) || millisecondsToTimeout is not IntegerOperator millisecondsToTimeoutInteger)
        {
            throw new Exception("Cannot build HtmlFlowInputSchemaJson from prism as the timeout was missing or was not an integer.");
        }

        // this huge chunk of code seems complicated but all it is doing is mapping the headers
        //
        List<HtmlFlowHeaderSchemaJson> prismHeaders = [];
        //
        if(basePrism.TryGetValue(HEADERS_JSON_NAME, out var headers) && headers is ArrayOperator headersArray && headersArray != null)
        {
            List<OperatorValue>? headersList = headersArray.GetValue();
            if(headersList != null)
            {
                foreach(var deltaHeader in headersList)
                {
                    // each header should be a prism (representing a HtmlFlowHeaderSchemaJson)
                    if(deltaHeader is PrismOperator deltaPrismHeader)
                    {
                        var deltaPrismValue = deltaPrismHeader.GetValue();

                        if (deltaPrismValue != null)
                        {
                            if (!deltaPrismValue.TryGetValue("header-key", out var deltaHeaderKey) || deltaHeaderKey is not StringOperator deltaHeaderKeyString || deltaHeaderKeyString == null)
                            {
                                throw new Exception("At least one header key was unexpectedly missing, not a string or null.");
                            }
                            if (!deltaPrismValue.TryGetValue("header-values", out var deltaHeaderValue) || deltaHeaderValue is not ArrayOperator deltaHeaderValueArray || deltaHeaderValueArray == null || deltaHeaderValueArray.ArrayItemsSchema.Type != SchemaJson.JsonTypes.STRING)
                            {
                                throw new Exception("At least one header value was unexpectedly missing, not an array of strings or null.");
                            }

                            var deltaHeaderValueArrayValue = deltaHeaderValueArray.GetValue();

                            if (deltaHeaderValueArrayValue != null)
                            {
                                HtmlFlowHeaderSchemaJson newListItem = new()
                                {
                                    HeaderKey = deltaHeaderKeyString.GetValue(),
                                    HeaderValues = []
                                };

                                foreach (var deltaHeaderValueOperator in deltaHeaderValueArrayValue)
                                {
                                    if (deltaHeaderValueOperator is StringOperator deltaHeaderValueStringOperator)
                                    {
                                        string? toAdd = deltaHeaderValueStringOperator.GetValue();

                                        if (toAdd != null)
                                        {
                                            newListItem.HeaderValues.Add(toAdd);
                                        }
                                    }
                                }

                                if (newListItem.HeaderValues.Count != 0)
                                {
                                    prismHeaders.Add(newListItem);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("One of the headers unexpectedly had a null value.");
                        }
                    }
                }
            }
        }

        return new()
        {
            Url = urlString.GetValue(),
            Headers = prismHeaders,
            JsonPayload = payloadString.GetValue(),
            MillisecondsToTimeout = millisecondsToTimeoutInteger.GetValue()
        };
    }
}