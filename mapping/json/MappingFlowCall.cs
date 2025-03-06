using System.Text.Json;
using System.Text.Json.Serialization;


namespace luminary.mapping;

public class MappingFlowCallJson
{
    [JsonPropertyName("function")]
    public string? Function { get; set; }

    [JsonPropertyName("parameters")]
    public List<string>? Parameters { get; set; }
}