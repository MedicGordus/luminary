using System.Text.Json;
using System.Text.Json.Serialization;


namespace luminary.mapping;

public class MapperJson
{
    [JsonPropertyName("input-prism-schema")]
    public string? InputPrismSchema { get; set; }

    [JsonPropertyName("steps")]
    public Dictionary<ulong, MapperStepJson>? Steps { get; set; }


    public static Mapper Build(MapperJson _json)
    {
        return new Mapper(
            JsonSerializer.Deserialize<SchemaJson>(_json.InputPrismSchema ?? "") ?? throw new Exception("Could not parse json element into json schema."),
            MapperStepJson.Build(_json.Steps)
        );
    }
}