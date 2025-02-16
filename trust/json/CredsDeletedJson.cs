using System.Text.Json;
using System.Text.Json.Serialization;


namespace luminary.trust.json;

public class CredsDeletedJson
{
    [JsonPropertyName("public-keys-by-group")]
    public Dictionary<string, List<PublicKeyJson>>? PublicKeysByGroup { get; set; }
}