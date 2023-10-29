using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Community.Observability.Logging;

internal static class Extensions
{
    internal static IHostBuilder UseLogging(this IHostBuilder host, IConfiguration configuration)
    {
        host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .WriteTo.OpenTelemetry(options =>
            {
                options.Endpoint = $"{configuration.GetValue<string>("Otlp:Endpoint")}/v1/logs";
                options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc;
                options.ResourceAttributes = new Dictionary<string, object>
                {
                    ["service.name"] = configuration.GetValue<string>("Otlp:ServiceName")
                };
            }));
        return host;
    }

    internal static IApplicationBuilder UseLogging(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}