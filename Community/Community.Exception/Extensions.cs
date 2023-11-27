using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Community.Exception;

public static class Extensions
{
    public static IServiceCollection AddErrorMiddleware(this IServiceCollection services)
    {
        services.AddTransient<ErrorHandlingMiddleware>();
        return services;
    }
    
    public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
        return app;
    }
}