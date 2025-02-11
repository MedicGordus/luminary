using luminary.functions;
using luminary.mapping;

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
        //// test mapper usage
        //
        var _config = new MappingConfig(
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
                    0,
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
                    1,
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
                            Step = 1,
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
                    2,
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
                                    Function = "length('2'.'LengthString')",
                                    OutputParameter = "'Length'"
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
                                        "\"LengthSum\":" +
                                        "{" +
                                            "\"type\":\"integer\"" +
                                        "}" +
                                    "}" +
                                "}",
                            Step = 1,
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
        var _result = _config.Execute(
            JsonDocument.Parse(
                "{" +
                    "\"Test\":\"this is a test string that is 78 characters which should result in 78 + 2 = 80\"" +
                    //          0        1         2         3         4         5         6         7         8
                    //          0        0         0         0         0         0         0         0         0
                "}"
            )
        );
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