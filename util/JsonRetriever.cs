using System.Text.Json;

namespace luminary.util;

public static class JsonRetriever
{
    private static readonly HttpClient client = new();

    public static async Task<Scrubbable<T?>> GetJsonAsync<T>(string url) where T : class
    {
        Scrubbable<T?> _output = new();

        try
        {
            // Fetch the JSON string from the URL
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throws an exception for error status codes

            string jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string into an object of type T
            _output.ReturnValue = JsonSerializer.Deserialize<T>(jsonResponse);
        }
        catch (Exception _e)
        {
            _output.ActivateScrub(_e);
        }

        return _output;
    }
}