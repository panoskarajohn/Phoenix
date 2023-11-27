using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Initializers;

public static class Exentsions
{
    /// <summary>
    /// Add Initializer. 
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IInitializer
        => services.AddTransient<IInitializer, T>();
    
    /// <summary>
    /// Will execute all initializers
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseInitializers(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var initializers = scope.ServiceProvider.GetServices<IInitializer>();
        
        Task.Run(() =>
        {
            foreach (var initializer in initializers)
            {
                initializer.InitAsync().GetAwaiter().GetResult();
            }
        });
    
        return app;
    }
}