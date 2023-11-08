using Community.Context;
using Community.Extensions.Configuration;
using Community.Observability.Cors;
using Community.Observability.Health;
using Community.Observability.Logging;
using Community.Observability.Opentelemetry;
using Community.Observability.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Community.Observability;

public static class Extensions
{
    private const string AppSectionName = "app";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddObservability(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAppOptions(configuration)
            .AddContext()
            .AddRouting(opt => opt.LowercaseUrls = true)
            .AddCustomHealthChecks()
            .AddCORSPolicy(configuration)
            .AddOpenTelemetry(configuration);
        
        return services;
    }
    
    public static IServiceCollection AddAppOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var appOptions = configuration.GetOptions<AppOptions>("app");
        services = services
            .AddSingleton(appOptions);
        var version = appOptions.DisplayVersion ? $" {appOptions.Version}" : string.Empty;
        Console.WriteLine(Figgle.FiggleFonts.Doom.Render($"{appOptions.Name} v.{version}"));
        
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
        hostBuilder
            .UseLogging();
        return hostBuilder;
    }
}