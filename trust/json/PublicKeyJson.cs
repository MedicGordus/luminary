using System.Text.Json.Serialization;


namespace luminary.trust.json;

public class PublicKeyJson
{

    [JsonPropertyName("key-type")]
    public string? KeyType { get; set; }
    

    [JsonPropertyName("public-key-base64")]
    public string? PublicKeyBase64 { get; set; }
}