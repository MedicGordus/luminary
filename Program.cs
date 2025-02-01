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
            todo(),
            new List<MappingStepConfig> {
                new MappingStepConfig (1,MappingFunctions.Step1)
            }
        );
        //
        var _result = _config.Execute(jsonPayload);
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