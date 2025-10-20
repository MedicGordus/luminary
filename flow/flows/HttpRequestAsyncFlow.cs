using System.Net.Http.Headers;
using luminary.mapping;
using luminary.util;

namespace luminary.flow;


public class HttpRequestAsyncFlow : AsyncFlow
{
    
    public static readonly SchemaJson HtmlFlowInputSchema = new()
    {
        Type = SchemaJson.JsonTypes.OBJECT,
        Properties = new()
        {
            {
                HtmlFlowInputSchemaJson.URL_JSON_NAME, new SchemaJson()
                {
                    Title = "The url to be called.",
                    Type = SchemaJson.JsonTypes.STRING
                }
            },
            {
                HtmlFlowInputSchemaJson.PAYLOAD_JSON_NAME, new SchemaJson()
                {
                    Title = "The payload to send.",
                    Type = SchemaJson.JsonTypes.STRING
                }
            },
            {
                HtmlFlowInputSchemaJson.MILLISECONDS_TO_TIMEOUT_JSON_NAME, new SchemaJson()
                {
                    Title = "How many milliseconds to wait before timing out (less than zero, zero or null will wait forever, never will timeout).",
                    Type = SchemaJson.JsonTypes.INTEGER
                }
            },
            {
                HtmlFlowInputSchemaJson.HEADERS_JSON_NAME, new SchemaJson()
                {
                    Title = "The http headers to send.",
                    Description = "Contains a list of the http headers",
                    Type = SchemaJson.JsonTypes.ARRAY,
                    Items = new()
                    {
                        Type = SchemaJson.JsonTypes.OBJECT,
                        ObjectSchema = new()
                        {
                            Title = "Each of these objects represents one header.",
                            Description = "Each object has one key paired with an array of values that represent all the http headers to send.",
                            Type = SchemaJson.JsonTypes.OBJECT,
                            Properties = new()
                            {
                                {
                                    HtmlFlowHeaderSchemaJson.HEADER_KEY_JSON_NAME, new SchemaJson()
                                    {
                                        Title = "The http header key.",
                                        Type = SchemaJson.JsonTypes.STRING
                                    }
                                },
                                {
                                    HtmlFlowHeaderSchemaJson.HEADER_VALUES_JSON_NAME, new SchemaJson()
                                    {
                                        Title = "The http header value(s) for the corresponding key.",
                                        Type = SchemaJson.JsonTypes.ARRAY,
                                        Items = new()
                                        {
                                            Type = SchemaJson.JsonTypes.STRING
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    };

    public static readonly SchemaJson HtmlFlowOutputSchema = new()
    {
        Type = SchemaJson.JsonTypes.OBJECT,
        Properties = new()
        {
            {
                HtmlFlowOutputSchemaJson.STATUS_CODE_JSON_NAME, new SchemaJson()
                {
                    Title = "The http response status code.",
                    Description = "Contains the value of status code defined for HTTP defined in RFC 2616 for HTTP 1.1.",
                    Type = SchemaJson.JsonTypes.INTEGER
                }
            },
            {
                HtmlFlowOutputSchemaJson.PAYLOAD_JSON_NAME, new SchemaJson()
                {
                    Title = "The UTF-8 string payload, expected json.",
                    Description = "Contains the payload response from the http call.",
                    Type = SchemaJson.JsonTypes.STRING
                }
            },
            {
                HtmlFlowOutputSchemaJson.HEADERS_JSON_NAME, new SchemaJson()
                {
                    Title = "The http headers received from the request.",
                    Description = "Contains a list of the http headers",
                    Type = SchemaJson.JsonTypes.ARRAY,
                    Items = new()
                    {
                        Type = SchemaJson.JsonTypes.OBJECT,
                        ObjectSchema = new()
                        {
                            Title = "Each of these objects represents one header.",
                            Description = "Each object has one key paired with an array of values that represent all the http headers received.",
                            Type = SchemaJson.JsonTypes.OBJECT,
                            Properties = new()
                            {
                                {
                                    HtmlFlowHeaderSchemaJson.HEADER_KEY_JSON_NAME, new SchemaJson()
                                    {
                                        Title = "The http header key.",
                                        Type = SchemaJson.JsonTypes.STRING
                                    }
                                },
                                {
                                    HtmlFlowHeaderSchemaJson.HEADER_VALUES_JSON_NAME, new SchemaJson()
                                    {
                                        Title = "The http header value(s) for the corresponding key.",
                                        Type = SchemaJson.JsonTypes.ARRAY,
                                        Items = new()
                                        {
                                            Type = SchemaJson.JsonTypes.STRING
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    };

    protected readonly HttpClient Client;

    public HttpRequestAsyncFlow() : base()
    {
        Client = new HttpClient();
    }

    public override async Task<Panicable<Prism?>> ProcessAsync(Prism _input)
    {
        var output = new Panicable<Prism?>();

        try
        {
            // collect the input from the prism
            var input = HtmlFlowInputSchemaJson.BuildFromPrism(_input);

            // setup the http request
            var request = new HttpRequestMessage(HttpMethod.Post, input.Url);

            // apply headers
            if (input.Headers != null && input.Headers.Count != 0)
            {
                foreach (var deltaHeaderKey in input.Headers)
                {
                    if (deltaHeaderKey.HeaderValues != null && deltaHeaderKey.HeaderValues.Count != 0)
                    {
                        foreach (var deltaHeaderValue in deltaHeaderKey.HeaderValues)
                        {
                            if (deltaHeaderKey.HeaderKey != null && deltaHeaderValue != null)
                            {
                                request.Headers.Add(deltaHeaderKey.HeaderKey, deltaHeaderValue);
                            }
                        }
                    }
                }
            }

            // setup the content to send
            request.Content = input.JsonPayload == null ? null : new StringContent(
                input.JsonPayload,
                System.Text.Encoding.UTF8,
                "application/json"
            );

            // set timeout if configured
            if (input.MillisecondsToTimeout != null && input.MillisecondsToTimeout > 0)
            {
                Client.Timeout = TimeSpan.FromMilliseconds(input.MillisecondsToTimeout.Value);
            }
            else
            {
                // configuration has infinate timeout set
                Client.Timeout = Timeout.InfiniteTimeSpan;
            }


            // Send the request
            var response = await Client.SendAsync(request);


            // gather the reply into our object
            //
            HtmlFlowOutputSchemaJson httpResponse = new()
            {
                StatusCode = (int)response.StatusCode,
                Payload = await response.Content.ReadAsStringAsync().ConfigureAwait(false),
                Headers = []
            };
            //
            foreach (var deltaHeader in response.Headers)
            {
                httpResponse.Headers.Add(new()
                {
                    HeaderKey = deltaHeader.Key,
                    HeaderValues = [.. deltaHeader.Value]
                });
            }

            // set the reply in the expected prism format
            output.ReturnValue = new Prism(httpResponse.BuildOperator(), HtmlFlowOutputSchema);
        }
        catch (Exception e)
        {
            output.ActivatePanic(e);
        }

        return output;
    }
}