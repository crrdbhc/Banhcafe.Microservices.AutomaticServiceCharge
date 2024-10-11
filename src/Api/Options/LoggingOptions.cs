using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;

namespace Banhcafe.Microservices.ServiceChargingSystem.Api.Options;
public static class LogginOptions
{
    public static IHostBuilder AddLoggingOptions(this WebApplicationBuilder builder)
    {
        return builder.Host.UseSerilog(
            static (context, configuration) =>
            {
                var elasticSearchUrl = context.Configuration["Serilog:ElasticSearchUrl"]!;
                var applicationName =
                    context.Configuration["ApplicationName"] ?? "ServiceChargingSystem";

                configuration
                    .Enrich.With<SensitiveContentEnricher>()
                    .Enrich.FromLogContext()
                    .Enrich.WithClientIp()
                    .Enrich.WithCorrelationId(
                        headerName: "x-correlation-id",
                        addValueIfHeaderAbsence: true
                    );

                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .WriteTo.Console(new JsonFormatter())
                    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                    .MinimumLevel.Debug();

                configuration.WriteTo.Elasticsearch(
                    [new Uri(elasticSearchUrl!)],
                    opts =>
                    {
                        opts.MinimumLevel = LogEventLevel.Debug;
                        opts.DataStream = new DataStreamName(
                            $"{applicationName}-logs",
                            context.HostingEnvironment.EnvironmentName,
                            DateTimeOffset.Now.ToString("yyyy-MM")
                        );
                        opts.BootstrapMethod = BootstrapMethod.None;
                        opts.ConfigureChannel = channelOpts =>
                            channelOpts.BufferOptions = new BufferOptions { ExportMaxRetries = 4, };
                    },
                    transport => { }
                );
            }
        );
    }

    private class SensitiveContentEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Level is not LogEventLevel.Debug)
            {
                logEvent.RemovePropertyIfPresent("Documents");
            }
        }
    }
}