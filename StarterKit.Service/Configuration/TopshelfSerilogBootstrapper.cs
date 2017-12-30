namespace StarterKit.Service.Configuration
{
    using MassTransit.SerilogIntegration;
    using Serilog;
    using System;
    using System.Diagnostics;
    using Topshelf.Logging;

    public class TopshelfSerilogBootstrapper
    {
        public TopshelfSerilogBootstrapper()
        {
            // Common Initial
            var loggerConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithMachineName()
                .MinimumLevel.Is(Serilog.Events.LogEventLevel.Debug)
                .MinimumLevel.Override("MassTransit", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.Console()
                .WriteTo.AzureDocumentDB(
                    "https://<yourcosmodb>.documents.azure.com:443/",
                    Environment.GetEnvironmentVariable("CosmoDbAuthKey"),
                    "Logs",
                    "StarterKitDoSomeWork",
                    timeToLive: 86400);

            // Common Final
            Log.Logger = loggerConfiguration.CreateLogger();

            // Configure Topshelf Logger
            SerilogLogWriterFactory.Use(Log.Logger);

            // MassTransit to use Serilog
            SerilogLogger.Use();
        }
    }
}
