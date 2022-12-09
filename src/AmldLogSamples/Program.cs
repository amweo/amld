using Amld.Extensions.Logging;
using Amld.Extensions.Logging.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

Enhancer.Current.AppId = "amld.log.samples";

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile($"appsettings.json", false, true);
    })
    .ConfigureLogging((context, logging) => {
        logging.ClearProviders();
        logging.AddAmldLogger()
        .AddKafkaWriter(context.Configuration);
    }).Build();



Enhancer.Current.ChainId = Guid.NewGuid().ToString("N");
Enhancer.Current.TraceId = Guid.NewGuid().ToString("N");
Enhancer.Current.ParentTraceId= Guid.NewGuid().ToString("N");

var logger = host.Services.GetRequiredService<ILogger<Program>>();

logger.LogDebug(1, "Does this line get hit?");    // Not logged
logger.LogInformation(3, "Nothing to see here."); // Logs in ConsoleColor.DarkGreen
logger.LogWarning(5, "Warning... that was odd."); // Logs in ConsoleColor.DarkCyan
logger.LogError(7, "Oops, there was an error.");  // Logs in ConsoleColor.DarkRed
logger.LogCritical("应用程序崩溃!");
logger.LogTrace(5!, "== 120.");                   // Not logged

await host.RunAsync();