using Community.Extensions.Configuration;
using Community.Observability.Logging.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Community.Observability.Opentelemetry;

internal static class Extensions
{
    private const string OpenTelemetrySectionName = "Otlp";
    internal static IServiceCollection AddOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        var otlp = configuration.GetOptions<OpenTelemetryOptions>(OpenTelemetrySectionName);
        if (!otlp.Enabled)
        {
            return services;
        }
        
        Action<ResourceBuilder> appResourceBuilder =
            resource => resource
                .AddTelemetrySdk()
                .AddService(otlp.ServiceName);

        services.AddOpenTelemetry()
            .ConfigureResource(appResourceBuilder)
            .WithTracing(builder => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource("APITracing")
                //.AddConsoleExporter()
                .AddOtlpExporter(options => options.Endpoint = new Uri(otlp.Endpoint))
            )
            .WithMetrics(builder => builder
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter(options => options.Endpoint = new Uri(otlp.Endpoint)));
        return services;
    }
}