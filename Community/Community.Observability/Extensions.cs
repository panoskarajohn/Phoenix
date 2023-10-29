using Community.Context;
using Community.Observability.Cors;
using Community.Observability.Health;
using Community.Observability.Logging;
using Community.Observability.Opentelemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Community.Observability;

public static class Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddObservability(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddContext()
            .AddRouting(opt => opt.LowercaseUrls = true)
            .AddCustomHealthChecks()
            .AddCORSPolicy(configuration)
            .AddOpenTelemetry(configuration);
        
        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseObservability(this IApplicationBuilder app)
    {
        app
            .UseContext()
            .UseLogging();
        return app;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="endpointRouteBuilder"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder UseCustomEndpointRouteObservabilty(
        this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.UseCustomHealthChecks();
        return endpointRouteBuilder;
    }

    public static IHostBuilder UseObservability(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        hostBuilder.UseLogging(configuration);
        return hostBuilder;
    }
}