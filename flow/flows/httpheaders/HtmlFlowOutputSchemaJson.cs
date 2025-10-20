using System.Text.Json;
using System.Text.Json.Serialization;

using luminary.mapping;
using luminary.mapping.functions;

namespace luminary.flow;

public class HtmlFlowOutputSchemaJson
{
    /// <summary>
    /// The http response status code.
    /// </summary>
    [JsonPropertyName(STATUS_CODE_JSON_NAME)]
    public int? StatusCode { get; set; }
    public const string STATUS_CODE_JSON_NAME = "status-code";

    /// <summary>
    /// The UTF-8 string payload, expected json.
    /// </summary>
    [JsonPropertyName(PAYLOAD_JSON_NAME)]
    public string? Payload { get; set; }
    public const string PAYLOAD_JSON_NAME = "payload";

    /// <summary>
    /// The http headers received.
    /// </summary>
    [JsonPropertyName(HEADERS_JSON_NAME)]
    public List<HtmlFlowHeaderSchemaJson>? Headers { get; set; }
    public const string HEADERS_JSON_NAME = "headers";

    public PrismOperator BuildOperator()
    {
        // this block of code seems long but its just mapping the headers into the PrismOperator appropriately
        //
        List<OperatorValue> headerList = [];
        //
        if(Headers != null)
        {
            foreach (var deltaHeader in Headers)
            {
                if(deltaHeader.HeaderValues != null)
                {
                    // gather the headers into the operatorvalue list
                    var headerValueList = new List<OperatorValue>();
                    foreach (var deltaHeaderValue in deltaHeader.HeaderValues)
                    {
                        headerValueList.Add(new StringOperator(deltaHeaderValue));
                    }

                    // check if there are any values
                    if (headerValueList.Count != 0)
                    {
                        // build the header key
                        StringOperator headerKey = new StringOperator(deltaHeader.HeaderKey);

                        // build the header values
                        var headerValues = new ArrayOperator(
                            OperatorValue.OperatorValueType.String,
                            headerValueList,
                            HttpRequestAsyncFlow.HtmlFlowOutputSchema.Properties?[HEADERS_JSON_NAME]?.Items?.ObjectSchema?.Properties?[HtmlFlowHeaderSchemaJson.HEADER_VALUES_JSON_NAME].Items ?? throw new Exception("A constant value that should not be null returned null 549387.")
                        );

                        // build and add a new prism to the list of headers
                        headerList.Add(new PrismOperator(
                            new()
                            {
                                { HtmlFlowHeaderSchemaJson.HEADER_KEY_JSON_NAME, headerKey
                                },
                                { HtmlFlowHeaderSchemaJson.HEADER_VALUES_JSON_NAME, headerValues
                                }
                            }
                        ));
                    }
                }
            }
        }

        // build the headers into the array
        var headersArray = new ArrayOperator(
            OperatorValue.OperatorValueType.Prism,
            headerList,
            HttpRequestAsyncFlow.HtmlFlowOutputSchema.Properties?[HEADERS_JSON_NAME].Items ?? throw new Exception("A constant value that should not be null returned null 549387.")
        );

        // return the prism
        return new PrismOperator(
            new()
            {
                { STATUS_CODE_JSON_NAME, new StringOperator(Payload) },
                { PAYLOAD_JSON_NAME, new StringOperator(Payload) },
                { HEADERS_JSON_NAME, headersArray }
            }
        );
    }
}
