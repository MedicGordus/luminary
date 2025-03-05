using System.Text.Json.Serialization;


namespace luminary.util;

public class AppConfigJson
{
    [JsonPropertyName("listen-address")]
    public string? ListenAddress { get; set; }

    [JsonPropertyName("deployment-configuration-path")]
    public string? DeploymentConfigurationPath { get; set; }
}