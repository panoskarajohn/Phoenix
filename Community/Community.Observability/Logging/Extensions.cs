using Community.Context;
using Community.Context.Abstractions;
using Community.Extensions.Configuration;
using Community.Observability.Logging.Options;
using Community.Observability.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace Community.Observability.Logging;

public static class Extensions
{
    private const string ConsoleOutputTemplate = "{Timestamp:HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}";

    private const string FileOutputTemplate =
        "{Timestamp:HH:mm:ss} [{Level:u3}] ({SourceContext}.{Method}) {Message}{NewLine}{Exception}";
    
    private const string AppSectionName = "app";
    private const string LoggerSectionName = "logger";
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="host"></param>
    /// <param name="configure"></param>
    /// <param name="loggerSectionName"></param>
    /// <param name="appSectionName"></param>
    /// <returns></returns>
    internal static IHostBuilder UseLogging(this IHostBuilder host,
        Action<LoggerConfiguration>? configure = null,
        string loggerSectionName = LoggerSectionName,
        string appSectionName = AppSectionName)
    {
        host.UseSerilog((context, loggerConfiguration) =>
        {
            if (string.IsNullOrWhiteSpace(loggerSectionName))
            {
                loggerSectionName = LoggerSectionName;
            }

            if (string.IsNullOrWhiteSpace(appSectionName))
            {
                appSectionName = AppSectionName;
            }

            var appOptions = context.Configuration.GetOptions<AppOptions>(appSectionName);
            var loggerOptions = context.Configuration.GetOptions<LoggerOptions>(loggerSectionName);
            var openTelemetryOptions = context.Configuration.GetOptions<OpenTelemetryOptions>("Otlp");

            MapOptions(loggerOptions, appOptions, openTelemetryOptions, loggerConfiguration, context.HostingEnvironment.EnvironmentName);
            configure?.Invoke(loggerConfiguration);
        });
        return host;
    }
    
    private static void MapOptions(LoggerOptions loggerOptions, 
        AppOptions appOptions, 
        OpenTelemetryOptions openTelemetryOptions,
        LoggerConfiguration loggerConfiguration, 
        string environmentName)
    {
        var level = GetLogEventLevel(loggerOptions.Level);

        loggerConfiguration.Enrich.FromLogContext()
            .MinimumLevel.Is(level)
            .Enrich.WithProperty("Environment", environmentName)
            .Enrich.WithProperty("Application", appOptions.Name)
            .Enrich.WithProperty("Instance", appOptions.Instance)
            .Enrich.WithProperty("Version", appOptions.Version);

        foreach (var (key, value) in loggerOptions.Tags ?? new Dictionary<string, object>())
        {
            loggerConfiguration.Enrich.WithProperty(key, value);
        }

        foreach (var (key, value) in loggerOptions.Overrides ?? new Dictionary<string, string>())
        {
            var logLevel = GetLogEventLevel(value);
            loggerConfiguration.MinimumLevel.Override(key, logLevel);
        }

        loggerOptions.ExcludePaths?.ToList().ForEach(p => loggerConfiguration.Filter
            .ByExcluding(Matching.WithProperty<string>("RequestPath", n => n.EndsWith(p))));

        loggerOptions.ExcludeProperties?.ToList().ForEach(p => loggerConfiguration.Filter
            .ByExcluding(Matching.WithProperty(p)));

        Configure(loggerConfiguration, loggerOptions, appOptions, openTelemetryOptions);
    }
    
    private static void Configure(LoggerConfiguration loggerConfiguration, 
        LoggerOptions options, 
        AppOptions appOptions, 
        OpenTelemetryOptions openTelemetryOptions)
    {
        var consoleOptions = options.Console ?? new ConsoleOptions();
        var fileOptions = options.File ?? new Options.FileOptions();
        openTelemetryOptions = openTelemetryOptions ?? new OpenTelemetryOptions();

        if (consoleOptions.Enabled)
        {
            loggerConfiguration.WriteTo.Console(outputTemplate: ConsoleOutputTemplate);
        }

        if (fileOptions.Enabled)
        {
            var path = string.IsNullOrWhiteSpace(fileOptions.Path) ? "logs/logs.txt" : fileOptions.Path;
            if (!Enum.TryParse<RollingInterval>(fileOptions.Interval, true, out var interval))
            {
                interval = RollingInterval.Day;
            }

            loggerConfiguration.WriteTo.File(path, rollingInterval: interval, outputTemplate: FileOutputTemplate);
        }

        if (openTelemetryOptions.Enabled)
        {
            loggerConfiguration.WriteTo.OpenTelemetry(options =>
            {
                options.Endpoint = $"{options.Endpoint}/v1/logs";
                options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc;
                options.ResourceAttributes = new Dictionary<string, object>
                {
                    ["service.name"] = appOptions.Name
                };
            });
        }
    }
    
    private static LogEventLevel GetLogEventLevel(string level)
        => Enum.TryParse<LogEventLevel>(level, true, out var logLevel)
            ? logLevel
            : LogEventLevel.Information;
    
    public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
    {
        app.Use(async (ctx, next) =>
        {
            var logger = ctx.RequestServices.GetRequiredService<ILogger<IContext>>();
            var context = ctx.RequestServices.GetRequiredService<IContext>();
            logger.LogInformation(
                "Started processing a request [Request ID: '{RequestId}', Correlation ID: '{CorrelationId}', Trace ID: '{TraceId}', User ID: '{UserId}']...",
                context.RequestId, context.CorrelationId, context.TraceId, context.UserId);

            await next();

            logger.LogInformation(
                "Finished processing a request with status code: {StatusCode} [Request ID: '{RequestId}', Correlation ID: '{CorrelationId}', Trace ID: '{TraceId}', User ID: '{UserId}']",
                ctx.Response.StatusCode, context.RequestId, context.CorrelationId, context.TraceId,
                context.UserId);
        });

        return app;
    }
}