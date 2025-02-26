
using System.Text.Json;
using System.Text.Json.Serialization;

namespace luminary.data.replication;

public class HealthStatusJson
{
    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("configured-precedence")]
    public SpookPrecedence ConfiguredPrecedence { get; set; }

    [JsonPropertyName("current-precedence")]
    public SpookPrecedence CurrentPrecedence { get; set; }
    
    [JsonPropertyName("current-tangled-state")]
    public SpookTangledState CurrentTangledState  { get; set; }

    [JsonPropertyName("tangle-health")]
    public List<HealthStatusJson>? TangleHealth { get; set; }

    public string? ToJsonString()
    {
        try
        {
            return JsonSerializer.Serialize(this);
        }
        catch
        {
            return null;
        }
    }

    public static HealthStatusJson? Parse(string _json)
    {
        try
        {
            return JsonSerializer.Deserialize<HealthStatusJson>(_json);
        }
        catch
        {
            return null;
        }
    }
}