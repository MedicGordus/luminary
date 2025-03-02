using luminary.mapping.functions;
using luminary.mapping;
using luminary.util;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.CommandLine;
using System.Text.Json;
using System.Xml.Linq;
using System.Net;


namespace luminary;

class Program
{
    public static string? Path;
    public static Task HttpHostTask;

    public static IHost? HttpHost;


    static async Task Main(string[] _args)
    {
        //// handle command arguments
        //
        var rootCommand = new RootCommand("Luminary");
        //
        var pathOption = new Option<string>(
            name: "--path",
            description: "Path to where all files are located"
        ) { IsRequired = true };
        rootCommand.AddOption(pathOption);
        //
        rootCommand.SetHandler((_path) =>
        {
            Path = _path;
        }, pathOption);
        //
        await rootCommand.InvokeAsync(_args);
        //
        if(Path == null || !Directory.Exists(Path))
        {
            Console.WriteLine("Path null or invalid. Exiting.");
            return;
        }
        //
        ////

        RunTests();

        //// https listener
        //
        var builder = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(_webBuilder =>
            {
                _webBuilder.Configure(_app =>
                {
                    _app.UseRouting();

                    _app.UseEndpoints(_endpoints =>
                    {
                        _endpoints.MapGet("{*path}", HandleHttpGetAsync);
                        _endpoints.MapPost("{*path}", HandleHttpPostAsync);
                    });
                })
                .UseUrls("http://localhost:80");
            });
        //
        HttpHost = builder.Build();
        HttpHostTask = HttpHost.RunAsync();
        //
        ////
        

        // temporary to hold the application open lol
        await HttpHostTask.ConfigureAwait(false);

        // how to stop host
        await HttpHost.StopAsync();
    }

    private static async Task HandleHttpGetAsync(HttpContext _context)
    {
        var path = _context.Request.Path.Value?.TrimStart('/') ?? "(empty)";
        var request = _context.Request;
        var query = request.Query;

        var responseData = new Dictionary<string, string>
        {
            {"path", path},
            {"message", "Default Message"},
            {"name", "Unknown"}
        };

        if (query.TryGetValue("message", out var messageValue))
        {
            responseData["message"] = messageValue.ToString();
        }
        if (query.TryGetValue("name", out var nameValue))
        {
            responseData["name"] = nameValue.ToString();
        }

        _context.Response.ContentType = "application/json";
        _context.Response.StatusCode = (int)HttpStatusCode.OK;
        await _context.Response.WriteAsync(JsonSerializer.Serialize(responseData));
    }
    
    private static async Task HandleHttpPostAsync(HttpContext _context)
    {
        var path = _context.Request.Path.Value?.TrimStart('/') ?? "(empty)";
        var request = _context.Request;
        var query = request.Query;

        var responseData = new Dictionary<string, string>
        {
            {"path", path},
            {"message", "Default Message"},
            {"name", "Unknown"}
        };

        if (query.TryGetValue("message", out var messageValue))
        {
            responseData["message"] = messageValue.ToString();
        }
        if (query.TryGetValue("name", out var nameValue))
        {
            responseData["name"] = nameValue.ToString();
        }

        _context.Response.ContentType = "application/json";
        _context.Response.StatusCode = (int)HttpStatusCode.OK;
        await _context.Response.WriteAsync(JsonSerializer.Serialize(responseData));
    }

    public static void RunTests()
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
    }
}