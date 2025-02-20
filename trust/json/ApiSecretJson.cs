using luminary.util;

using System.Text.Json.Serialization;


namespace luminary.trust.json;

public class ApiSecretJson
{
    [JsonPropertyName("secret")]
    public string? Secret { get; set; }

    [JsonPropertyName("organization-url")]
    public string? OrganizationUrl { get; set; }

    [JsonPropertyName("organization-group")]
    public string? OrganizationGroup { get; set; }
}