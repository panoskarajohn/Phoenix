using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Community.Observability.Opentelemetry;

internal static class Extensions
{
    internal static IServiceCollection AddOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        Action<ResourceBuilder> appResourceBuilder =
            resource => resource
                .AddTelemetrySdk()
                .AddService(configuration.GetValue<string>("Otlp:ServiceName"));

        services.AddOpenTelemetry()
            .ConfigureResource(appResourceBuilder)
            .WithTracing(builder => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource("APITracing")
                //.AddConsoleExporter()
                .AddOtlpExporter(options => options.Endpoint = new Uri(configuration.GetValue<string>("Otlp:Endpoint")))
            )
            .WithMetrics(builder => builder
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter(options => options.Endpoint = new Uri(configuration.GetValue<string>("Otlp:Endpoint"))));
        return services;
    }
}