using System.Net;
using System.Text.Json;
using luminary.util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace luminary.data.flux;


public class Listener
{
    public static Panicable<IHost> StartHost(string? _address)
    {
        Panicable<IHost> output = new();

        try
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(_webBuilder =>
                {
                    _webBuilder.Configure(
                        _app =>
                        {
                            _app.UseRouting();

                            _app.UseEndpoints(
                                _endpoints =>
                                {
                                    _endpoints.MapGet("{*path}", HandleHttpGetAsync);
                                    _endpoints.MapPost("{*path}", HandleHttpPostAsync);
                                }
                            );
                        }
                    )
                    .UseUrls(_address ?? "");
                });

            output.ReturnValue = builder.Build();
        }
        catch(Exception e)
        {
            output.ActivatePanic(e);
        }

        return output;
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
        await _context.Response.WriteAsync(JsonSerializer.Serialize(responseData)).ConfigureAwait(false);
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
        await _context.Response.WriteAsync(JsonSerializer.Serialize(responseData)).ConfigureAwait(false);
    }
}