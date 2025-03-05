using System.Net.Http.Headers;
using System.Text.Json.Serialization;


namespace luminary.mapping;

public class MapperStepJson
{
    [JsonPropertyName("step")]
    public ulong Step { get; set; }

    [JsonPropertyName("output-prism-schema")]
    public string? OutputPrismSchema { get; set; }

    [JsonPropertyName("step-actions")]
    public List<ParameterMapJson>? StepActions { get; set; }

    public static Dictionary<ulong, MapperStep> Build(Dictionary<ulong, MapperStepJson>? _steps)
    {
        Dictionary<ulong, MapperStep> output = [];

        if (_steps != null && _steps.Count != 0)
        {
            foreach (KeyValuePair<ulong, MapperStepJson> deltaJson in _steps)
            {
                output.Add(
                    deltaJson.Key,
                    new MapperStep(deltaJson.Value)
                );
            }
        }

        return output;
    }
}