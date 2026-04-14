using luminary.util;


using System.Text.Json;
using System.Text.Json.Serialization;


namespace luminary.trust.json;
/// <summary>
/// This is the object sent to the auth update endpoint of luminary as an indicator they need to update their keys for this organization.
/// </summary>
/// <remarks>To prevent bad actors from spamming valid changes, valid payloads with previously received paired datetime and nuance are ignored.</remarks>
public class AuthUpdateJson
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("datetime-stamp")]
    public string? DatetimeStamp { get; set; }

    [JsonPropertyName("nuance")]
    public string? Nuance { get; set; }

    [JsonPropertyName("public-key")]
    public PublicKeyJson? PublicKey { get; set; }

    [JsonPropertyName("signature-base64")]
    public string? SignatureBase64 { get; set; }

    public string? GetSignableData()
    {
        if (Url == null || Nuance == null || DatetimeStamp == null)
        {
            return null;
        }

        return Url + Nuance;
    }
}