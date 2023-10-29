using Community.Context.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Community.Context;

public static class Extensions
{
    public static IServiceCollection AddContext(this IServiceCollection services)
    {
        services.AddSingleton<ContextAccessor>();
        services.AddTransient(sp => sp.GetRequiredService<ContextAccessor>().Context);
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }

    public static IApplicationBuilder UseContext(this IApplicationBuilder app)
    {
        app.UseCorrelationId();
        app.Use((ctx, next) =>
        {
            ctx.RequestServices.GetRequiredService<ContextAccessor>().Context = new Context.Context(ctx);
            return next();
        });
        return app;
    }
}