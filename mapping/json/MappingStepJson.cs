using System.Net.Http.Headers;
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

    public static Dictionary<ulong, MappingStepConfig> Build(Dictionary<ulong, MappingStepJson>? _steps)
    {
        Dictionary<ulong, MappingStepConfig> output = [];

        if(_steps != null && _steps.Count != 0)
        {        
            foreach(KeyValuePair<ulong, MappingStepJson> _deltaJson in _steps)
            {
                output.Add(
                    _deltaJson.Key,
                    new MappingStepConfig(_deltaJson.Value)
                );
            }
        }

        return output;
    }
}