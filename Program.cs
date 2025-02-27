using luminary.mapping.functions;
using luminary.mapping;
using luminary.util;

using System;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;


namespace luminary;

class Program
{
    static void Main(string[] args)
    {
        //// test totp
        //
        Console.WriteLine(
            string.Format(
                "otp = {0}",
                HmacSha256.ComputeTotp([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10], Epoch.GetMillisecondsSinceUnixEpoch())
            )
        );
        //
        ////

        Console.WriteLine("To improve efficiency, we need a method that builds dummy inputs, runs the mapper, collects the fields that are referenced and then adjusts the mapping to ONLY map and fill those fields - to ignore the other ones, even if configured to map them.");


        //// test mapper usage
        //
        string _jsonTest = """
        {
            "input-prism-schema":"{
                \"type\":\"object\",
                \"properties\":
                {
                    \"Test\":
                    {
                        \"type\":\"string\"
                    }
                }
            }",
            "steps":
            {
                "1":{
                    "step":1,
                    "output-prism-schema":"{
                        \"type\":\"object\",
                        \"properties\":
                        {
                            \"Length\":
                            {
                                \"type\":\"integer\"
                            }
                        }
                    }",
                    "step-actions":[
                        {
                            "output-parameter":"'Length'",
                            "function":"length('0'.'Test')"
                        }
                    ]
                },
                "2":{
                    "step":2,
                    "output-prism-schema":"{
                        \"type\":\"object\",
                        \"properties\":
                        {
                            \"LengthString\":
                            {
                                \"type\":\"string\"
                            }
                        }
                    }",
                    "step-actions":[
                        {
                            "output-parameter":"'LengthString'",
                            "function":"tostring('1'.'Length')"
                        }
                    ]
                },
                "3":{
                    "step":3,
                    "output-prism-schema":"{
                        \"type\":\"object\",
                        \"properties\":
                        {
                            \"Length\":
                            {
                                \"type\":\"integer\"
                            }
                        }
                    }",
                    "step-actions":[
                        {
                            "output-parameter":"'Length'",
                            "function":"length('2'.'LengthString')"
                        }
                    ]
                },
                "4":{
                    "step":3,
                    "output-prism-schema":"{
                        \"type\":\"object\",
                        \"properties\":
                        {
                            \"LengthSum\":
                            {
                                \"type\":\"integer\"
                            }
                        }
                    }",
                    "step-actions":[
                        {
                            "output-parameter":"'LengthSum'",
                            "function":"add('1'.'Length','3'.'Length')"
                        }
                    ]
                }
            }
        }
        """;
        _jsonTest = _jsonTest.Replace("\r\n", "");
        var _config = MappingConfigJson.Build((MappingConfigJson)JsonSerializer.Deserialize<MappingConfigJson>(_jsonTest));
        //
        string? _resultingTestPayload = _config.Execute(
            JsonDocument.Parse(
                "{" +
                    "\"Test\":\"this is a test string that is 78 characters which should result in 78 + 2 = 80\"" +
                //          0        1         2         3         4         5         6         7         8
                //          0        0         0         0         0         0         0         0         0
                "}"
            )
        ).BuildJsonStringPayload();
        //
        ////

        //// test2 mapper usage
        //
        var _config2 = new MappingConfig(
            JsonDocument.Parse(
                    "{" +
                        "\"type\":\"object\"," +
                        "\"properties\":" +
                        "{" +
                            "\"Test\":" +
                            "{" +
                                "\"type\":\"string\"" +
                            "}" +
                        "}" +
                    "}"
                ),
                new Dictionary<ulong, MappingStepConfig> {
                {
                    1,
                    new MappingStepConfig(
                        new MappingStepJson() {
                            OutputPrismSchema =
                                "{" +
                                    "\"type\":\"object\"," +
                                    "\"properties\":" +
                                    "{" +
                                        "\"Length\":" +
                                        "{" +
                                            "\"type\":\"integer\"" +
                                        "}" +
                                    "}" +
                                "}",
                            Step = 1,
                            StepActions = new List<ParameterMapJson> {
                                new ParameterMapJson() {
                                    Function = "length('0'.'Test')",
                                    OutputParameter = "'Length'"
                                }
                            }
                        }
                    )
                },
                {
                    2,
                    new MappingStepConfig(
                        new MappingStepJson() {
                            OutputPrismSchema =
                                "{" +
                                    "\"type\":\"object\"," +
                                    "\"properties\":" +
                                    "{" +
                                        "\"LengthString\":" +
                                        "{" +
                                            "\"type\":\"string\"" +
                                        "}" +
                                    "}" +
                                "}",
                            Step = 2,
                            StepActions = new List<ParameterMapJson> {
                                new ParameterMapJson() {
                                    Function = "tostring('1'.'Length')",
                                    OutputParameter = "'LengthString'"
                                }
                            }
                        }
                    )
                },
                {
                    3,
                    new MappingStepConfig(
                        new MappingStepJson() {
                            OutputPrismSchema =
                                "{" +
                                    "\"type\":\"object\"," +
                                    "\"properties\":" +
                                    "{" +
                                        "\"Length\":" +
                                        "{" +
                                            "\"type\":\"integer\"" +
                                        "}" +
                                    "}" +
                                "}",
                            Step = 3,
                            StepActions = new List<ParameterMapJson> {
                                new ParameterMapJson() {
                                    Function = "length('2'.'LengthString')",
                                    OutputParameter = "'Length'"
                                }
                            }
                        }
                    )
                },
                {
                    4,
                    new MappingStepConfig(
                        new MappingStepJson() {
                            OutputPrismSchema =
                                "{" +
                                    "\"type\":\"object\"," +
                                    "\"properties\":" +
                                    "{" +
                                        "\"LengthSum\":" +
                                        "{" +
                                            "\"type\":\"integer\"" +
                                        "}" +
                                    "}" +
                                "}",
                            Step = 4,
                            StepActions = new List<ParameterMapJson> {
                                new ParameterMapJson() {
                                    Function = "add('1'.'Length','3'.'Length')",
                                    OutputParameter = "'LengthSum'"
                                }
                            }
                        }
                    )
                }
            }
        );
        //
        string? _resultingPayload = _config2.Execute(
            JsonDocument.Parse(
                "{" +
                    "\"Test\":\"this is a test string that is 78 characters which should result in 78 + 2 = 80\"" +
                //          0        1         2         3         4         5         6         7         8
                //          0        0         0         0         0         0         0         0         0
                "}"
            )
        ).BuildJsonStringPayload();
        //
        ////

        //// https listener
        //
        var _builder = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.Configure(app =>
                {
                    app.UseRouting();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapGet("{*path}", HandlePathCallAsync);

                    });
                })
                .UseUrls("http://localhost:80");
            });
        //
        var _host = _builder.Build();
        _host.Run();
        //
        ////
    }

    /// <summary>
    /// Test function to handle https calls
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private static async Task HandlePathCallAsync(HttpContext context)
    {
        var _path = context.Request.Path.Value?.TrimStart('/') ?? "(empty)";
        var _request = context.Request;
        var _query = _request.Query;

        var _responseData = new Dictionary<string, string>
        {
            {"path", _path},
            {"message", "Default Message"},
            {"name", "Unknown"}
        };

        if (_query.TryGetValue("message", out var _messageValue))
        {
            _responseData["message"] = _messageValue.ToString();
        }
        if (_query.TryGetValue("name", out var _nameValue))
        {
            _responseData["name"] = _nameValue.ToString();
        }

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(_responseData));
    }
}