using System.Text.Json;

namespace luminary.util;

public static class JsonRetriever
{
    private static readonly HttpClient Client = new();

    public static async Task<Scrubbable<T?>> GetJsonAsync<T>(string _url) where T : class
    {
        Scrubbable<T?> output = new();

        try
        {
            // Fetch the JSON string from the URL
            HttpResponseMessage response = await Client.GetAsync(_url);
            response.EnsureSuccessStatusCode(); // Throws an exception for error status codes

            string jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string into an object of type T
            output.ReturnValue = JsonSerializer.Deserialize<T>(jsonResponse);
        }
        catch (Exception e)
        {
            output.ActivateScrub(e);
        }

        return output;
    }
}