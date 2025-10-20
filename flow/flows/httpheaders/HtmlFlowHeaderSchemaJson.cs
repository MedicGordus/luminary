using System.Text.Json;
using System.Text.Json.Serialization;
using luminary.mapping.functions;

namespace luminary.flow;

public class HtmlFlowHeaderSchemaJson
{
    /// <summary>
    /// The http header key.
    /// </summary>
    [JsonPropertyName(HEADER_KEY_JSON_NAME)]
    public string? HeaderKey { get; set; }
    public const string HEADER_KEY_JSON_NAME = "header-key";

    /// <summary>
    /// The http header value(s) for the corresponding key.
    /// </summary>
    [JsonPropertyName("header-values")]
    public List<string>? HeaderValues { get; set; }
    public const string HEADER_VALUES_JSON_NAME = "header-values";
}