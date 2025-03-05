using System.Text.Json;
using System.Threading.Tasks;

namespace luminary.util;

public static class AppConfig
{
    public const string PATH_CONFIG = "config.json";

    public static string? ListenAddress;

    public static string? DeploymentConfigurationPath;

    public static async Task<Panicable> LoadConfigAsync(string _basePath)
    {
        Panicable output = new();

        try
        {
            string filePath = Path.Combine(_basePath, PATH_CONFIG);
            string jsonFileContents = await File.ReadAllTextAsync(filePath);

            AppConfigJson? json = JsonSerializer.Deserialize<AppConfigJson>(jsonFileContents);

            if(json == null || json.ListenAddress == null)
            {
                output.ManuallyPanic($"listen-address missing in json file, or '{filePath}' file unable to parse to AppConfigJson.");

                return output;
            }

            ListenAddress = json.ListenAddress;

            DeploymentConfigurationPath = json.DeploymentConfigurationPath;
        }
        catch (Exception e)
        {
            output.ActivatePanic(e);
        }

        return output;
    }
}