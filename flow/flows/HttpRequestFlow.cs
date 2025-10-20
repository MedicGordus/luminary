using luminary.mapping;

namespace luminary.flow;


public class HtmlFlow : AsyncFlow
{
    protected readonly HttpClient Client;

    public HtmlFlow() : base()
    {
        Client = new HttpClient();
    }

    public static readonly SchemaJson HtmlFlowInputSchema = new()
    {
        Type = SchemaJson.JsonTypes.OBJECT,
        Properties = new()
        {
            {
                "url", new SchemaJson()
                {
                    Title = "The url to be called.",
                    Type = SchemaJson.JsonTypes.STRING
                }
            },
            {
                "", new SchemaJson()
                {

                }
            }
        }
    };

    public static readonly SchemaJson HemlFlowOutputSchema = new()
    {
        Type = SchemaJson.JsonTypes.OBJECT,
        Properties = new()
        {
            {
                "status-code", new SchemaJson()
                {
                    Title = "The http response status code.",
                    Description = "Contains the value of status code defined for HTTP defined in RFC 2616 for HTTP 1.1.",
                    Type = SchemaJson.JsonTypes.INTEGER
                }
            },
            {
                "payload", new SchemaJson()
                {
                    Title = "The UTF-8 string payload, expected json.",
                    Description = "Contains the payload response from the http call.",
                    Type = SchemaJson.JsonTypes.STRING
                }
            },
            {
                "headers", new SchemaJson()
                {
                    Title = "The http headers received from the request.",
                    Description = "Contains a list of the http headers",
                    Type = SchemaJson.JsonTypes.ARRAY,
                    ArrayType = SchemaJson.JsonTypes.OBJECT,
                    Properties = new()
                    {
                        {
                            ""
                        }
                    }
                }
            }
        }
    };

    public override async Task<Prism?> ProcessAsync(Prism _input)
    {
        string url = ;

        HttpResponseMessage response = await Client.PostAsync(
            url,
            new StringContent(
                System.Text.Json.JsonSerializer.Serialize(new { key = "value" }),
                System.Text.Encoding.UTF8,
                "application/json"
            )
        ).ConfigureAwait(false);


        int responseStatusCode = (int)response.StatusCode;
        string responsePayload = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        foreach (var header in response.Headers)
        {
            string s = $"{header.Key}: {string.Join(", ", header.Value)}";
        }
   

        todo();
    }
}