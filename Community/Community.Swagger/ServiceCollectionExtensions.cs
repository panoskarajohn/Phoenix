using Community.Extensions.Configuration;
using Community.Swagger.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Community.Swagger;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("swagger");
        var options = section.BindOptions<SwaggerOptions>();
        services.Configure<SwaggerOptions>(section);
        
        if (!options.Enabled)
        {
            return services;
        }

        services.AddApiVersioning();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.SchemaFilter<ExcludePropertiesFilter>();
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc(options.Version, new OpenApiInfo
            {
                Title = options.Title,
                Version = options.Version
            });
        });

        return services;
    }
    
    public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
    {
        var options = app.ApplicationServices.GetRequiredService<IOptions<SwaggerOptions>>().Value;
        if (!options.Enabled)
        {
            return app;
        }
        
        app.UseSwagger();
        app.UseReDoc(reDoc =>
        {
            reDoc.RoutePrefix = string.IsNullOrWhiteSpace(options.Route) ? "swagger" : options.Route;
            reDoc.SpecUrl($"/swagger/{options.Version}/swagger.json");
            reDoc.DocumentTitle = options.Title;
        });

        return app;
    }


}

