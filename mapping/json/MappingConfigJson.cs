using System.Text.Json;
using System.Text.Json.Serialization;


namespace luminary.mapping;

public class MappingConfigJson
{
    [JsonPropertyName("input-prism-schema")]
    public string? InputPrismSchema { get; set; }

    [JsonPropertyName("steps")]
    public Dictionary<ulong, MappingStepJson>? Steps { get; set; }


    public static MappingConfig Build(MappingConfigJson _json)
    {
        return new MappingConfig(
            JsonDocument.Parse(_json.InputPrismSchema ?? ""),
            MappingStepJson.Build(_json.Steps)
        );
    }
}