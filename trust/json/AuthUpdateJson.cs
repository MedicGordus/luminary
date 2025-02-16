using System.Text.Json;
using System.Text.Json.Serialization;


namespace luminary.trust.json;

/// <summary>
/// This is the object sent to the auth update endpoint of luminary as an indicator they need to update their keys for this organization.
/// </summary>
public class AuthUpdateJson
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("nuance")]
    public string? Nuance { get; set; }
    
    [JsonPropertyName("public-key")]
    public PublicKeyJson? PublicKey { get; set; }

    [JsonPropertyName("signature-base64")]
    public string? SignatureBase64 { get; set; }
}