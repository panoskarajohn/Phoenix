using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Community.Observability.Cors;

internal static class Extensions
{
    internal static IServiceCollection AddCORSPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var origins = new List<string>();
        configuration.Bind(AppSettings.CORS_MAIN, origins);

        services.AddCors(options =>
        {
            options.AddPolicy(name: Policies.CORS_MAIN,
                builder => builder.WithOrigins(origins.ToArray())
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        return services;
    }
}