using luminary.data.flux;
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
using System.Runtime.InteropServices;


namespace luminary;

class Program
{
    /// <summary>
    /// The root path passed into the Main() arguments.
    /// </summary>
    public static string? Path;

    /// <summary>
    /// This holds reference to the host task which is running parallel.
    /// </summary>
    public static Task? HttpHostTask;

    /// <summary>
    /// This holds reference to the http host listener for flux.
    /// </summary>
    public static IHost? HttpHost;

    /// <summary>
    /// This is cancelled when the app receives the command to shutdown.
    /// </summary>
    public static CancellationTokenSource ShutdownCts = new();

    /// <summary>
    /// This task is waiting on a parallel thread, waiting to process everything that needs to be shutdown before the app exits.
    /// </summary>
    public static Task? HandleShutdownTask;

    /// <summary>
    /// Program entry point.
    /// </summary>
    /// <param name="_args">Arguments passed into the app</param>
    static async Task Main(string[] _args)
    {
        //// app shutdown logic
        //
        HandleShutdownTask = Task.Run(HandleShutdownAsync);
        //
        // Handle Ctrl+C (all platforms)
        Console.CancelKeyPress += (_sender, _e) =>
        {
            Console.WriteLine("Ctrl+C detected. Shutting down...");
            ShutdownCts.Cancel();
            _e.Cancel = true; // Prevent immediate termination
        };
        //
        // Handle SIGTERM (Linux/macOS)
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            PosixSignalRegistration.Create(PosixSignal.SIGTERM, _context =>
            {
                Console.WriteLine("Received SIGTERM. Shutting down...");
                ShutdownCts.Cancel();
            });
        }
        //
        // Handle process exit (all platforms, but limited time on Windows)
        AppDomain.CurrentDomain.ProcessExit += (_sender, _e) =>
        {
            Console.WriteLine("Process exiting. Quick cleanup...");

            // Note: Limited time here (default ~2-3 seconds on Windows)
            ShutdownCts.Cancel();
        };
        //
        ////

        //// handle command arguments
        //
        var rootCommand = new RootCommand("Luminary");
        //
        var pathOption = new Option<string>(
            name: "--path",
            description: "Path to where core configuration files are located."
        ) { IsRequired = true };
        rootCommand.AddOption(pathOption);
        //
        rootCommand.SetHandler((_path) =>
        {
            Path = _path;
        }, pathOption);
        //
        await rootCommand.InvokeAsync(_args).ConfigureAwait(false);
        //
        if(Path == null || !Directory.Exists(Path))
        {
            Console.WriteLine("Path null or invalid.");
            Console.WriteLine("Because of the path issue: Exiting...");

            await FinalShutdownAsync().ConfigureAwait(false);

            return;
        }
        //
        ////
        
        //// load config
        //
        Panicable configResult = await AppConfig.LoadConfigAsync(Path).ConfigureAwait(false);
        //
        if(configResult.Paniced)
        {
            Console.WriteLine($"Issue occured during config load, here is the exception message: {configResult.GetException().Message}");
            Console.WriteLine("Because of the config load issue: Exiting...");

            await FinalShutdownAsync().ConfigureAwait(false);

            return;
        }
        else if(AppConfig.ListenAddress == null)
        {
            Console.WriteLine($"Config loaded null listen address ('listen-address')");
            Console.WriteLine("Because of the config load issue: Exiting...");

            await FinalShutdownAsync().ConfigureAwait(false);

            return;
        }
        else if(AppConfig.DeploymentConfigurationPath == null || !Directory.Exists(AppConfig.DeploymentConfigurationPath))
        {
            Console.WriteLine($"Config loaded null deployment configuration path ('deployment-configuration-path') or path '{AppConfig.DeploymentConfigurationPath}' doesn't exist.");
            Console.WriteLine("Because of the config load issue: Exiting...");

            await FinalShutdownAsync().ConfigureAwait(false);

            return;
        }
        //
        ////

        // temporary tests
        RunTests();

        //// https listener
        //
        Panicable<IHost> panicableResult = Listener.StartHost(AppConfig.ListenAddress);
        if(panicableResult.Paniced)
        {
            Console.WriteLine($"Issue occured during flux host listener, here is the exception message: {configResult.GetException().Message}");
            Console.WriteLine("Because of the flux host listener issue: Exiting...");

            await FinalShutdownAsync().ConfigureAwait(false);

            return;
        }
        //
        HttpHost = panicableResult.ReturnValue;
        if(HttpHost == null)
        {
            Console.WriteLine($"Flux host listener is unexpectedly null.");
            Console.WriteLine("Because of the flux host listener issue: Exiting...");

            await FinalShutdownAsync().ConfigureAwait(false);

            return;
        }
        HttpHostTask = Task.Run(() => HttpHost.RunAsync());
        //
        // temporary to hold the application open lol
        await HttpHostTask.ConfigureAwait(false);
        //
        ////

        // make sure everything shuts down before exiting
        await FinalShutdownAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// This ensures that the cancel is called and then returns the task to be awaited for shutdown.
    /// </summary>
    /// <remarks>
    /// This may seem overly complex, but it makes sure that any amount of calls will be safely awaitable.
    /// </remarks>
    private static Task FinalShutdownAsync()
    {
        // forces a cancel (could already be cancelled)
        ShutdownCts.Cancel();

        if(HandleShutdownTask != null)
        {
            // ensure everything shuts down ok
            return HandleShutdownTask;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// This runs on a parallel thread, waiting for the shutdown signal.
    /// 
    /// Once the signal is received, all shutdown calls are made to internal processes.
    /// </summary>
    private static async Task HandleShutdownAsync()
    {
        try
        {
            // -1 = wait forever, so we basically are just waitinf for the cancellation token to fire
            await Task.Delay(-1, ShutdownCts.Token).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            // stop host
            if(HttpHost != null)
            {
                await HttpHost.StopAsync().ConfigureAwait(false);
            }

            // stop everything else
        }
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
        string jsonTest = """
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
        jsonTest = jsonTest.Replace("\r\n", "");
        var config = MapperJson.Build(((MapperJson?)JsonSerializer.Deserialize<MapperJson>(jsonTest)) ?? throw new Exception());
        var prismOperator = Helper.BuildPrismFromJsonDocument(
            JsonDocument.Parse(
                "{" +
                    "\"Test\":\"this is a test string that is 78 characters which should result in 78 + 2 = 80\"" +
                //          0        1         2         3         4         5         6         7         8
                //          0        0         0         0         0         0         0         0         0
                "}"
            ),
            config.ExpectedInputPrismSchema
        );
        //
        MappingFlow testFlow1 = new MappingFlow(
            [
                new MappingFlowCallJson () {
                    Function = "goto",
                    Parameters = [
                        "1"
                    ]
                }
            ],
            new Dictionary<string, Mapper>() {
                { "1", config }
            }
        );
        //string? resultingTestPayload = config.Execute(prismOperator).BuildJsonStringPayload();
        string? resultingTestPayload = testFlow1.Process(
            new Prism(
                prismOperator,
                config.ExpectedInputPrismSchema
            ) ?? throw new Exception()
        )?.BuildJsonStringPayload();
        //
        ////

        //// test2 mapper usage
        //
        SchemaJson schema2 = JsonSerializer.Deserialize<SchemaJson>(
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
            )
        ) ?? throw new Exception();
        //
        var config2 = new Mapper(
                schema2,
                new Dictionary<ulong, MapperStep> {
                {
                    1,
                    new MapperStep(
                        new MapperStepJson() {
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
                    new MapperStep(
                        new MapperStepJson() {
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
                    new MapperStep(
                        new MapperStepJson() {
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
                                new() {
                                    Function = "length('2'.'LengthString')",
                                    OutputParameter = "'Length'"
                                }
                            }
                        }
                    )
                },
                {
                    4,
                    new MapperStep(
                        new MapperStepJson() {
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
                                new() {
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
        prismOperator = Helper.BuildPrismFromJsonDocument(
            JsonDocument.Parse(
                "{" +
                    "\"Test\":\"this is a test string that is 78 characters which should result in 78 + 2 = 80\"" +
                //          0        1         2         3         4         5         6         7         8
                //          0        0         0         0         0         0         0         0         0
                "}"
            ),
            schema2
        );
        //
        MappingFlow testFlow2 = new MappingFlow(
            [
                new MappingFlowCallJson () {
                    Function = "goto",
                    Parameters = [
                        "1"
                    ]
                }
            ],
            new Dictionary<string, Mapper>() {
                { "1", config2 }
            }
        );
        //
        //string? resultingPayload = config2.Execute(prismOperator).BuildJsonStringPayload();
        string? resultingTestPayload2 = testFlow2.Process(
            new Prism(
                prismOperator,
                config2.ExpectedInputPrismSchema
            ) ?? throw new Exception()
        )?.BuildJsonStringPayload();
        //
        ////
    }
}