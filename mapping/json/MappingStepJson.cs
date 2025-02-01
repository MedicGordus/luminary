using System.Text.Json.Serialization;


namespace luminary.mapping;

public class MappingStepJson
{
    [JsonPropertyName("step")]
    public ulong Step { get; set; }

    [JsonPropertyName("output-prism-schema")]
    public string? OutputPrismSchema { get; set; }

    [JsonPropertyName("step-actions")]
    public List<ParameterMapJson>? StepActions { get; set; }
}