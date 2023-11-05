using System.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Community.Context;

public static class HttpContextExtensions
{
    internal const string CorrelationIdKey = "correlation-id";

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        => app.Use(async (ctx, next) =>
        {
            if(ctx.Request.Headers.TryGetValue(CorrelationIdKey, out var correlationId))
            {
                var result = Guid.TryParse(correlationId, out var guid) ? guid : Guid.NewGuid(); 
                ctx.Items.Add(CorrelationIdKey, result);
                await next();
            }
            else
            {
                ctx.Items.Add(CorrelationIdKey, Guid.NewGuid());
                await next();
            }
        });

    public static Guid? TryGetCorrelationId(this HttpContext context)
        => context.Items.TryGetValue(CorrelationIdKey, out var id) ? (Guid) id : null;

    public static string? GetUserIpAddress(this HttpContext context)
    {
        if (context is null)
        {
            return string.Empty;
        }

        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        if (context.Request.Headers.TryGetValue("x-forwarded-for", out var forwardedFor))
        {
            var ipAddresses = forwardedFor.ToString().Split(",", StringSplitOptions.RemoveEmptyEntries);
            if (ipAddresses.Any())
            {
                ipAddress = ipAddresses[0];
            }
        }

        return ipAddress ?? string.Empty;
    }

    /// <summary>
    /// Some services will have implemented authentication and authorization,
    /// and will have a user id in the request context.
    /// For some we will have to get the user id from the request header x-user-id
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string? TryGetUserId(this HttpContext context)
    {
        if (context is null)
            return string.Empty;

        if (context.User?.Identity is not null)
        {
            return context.User.Identity.Name;
        }
        
        if(context.Request.Headers.TryGetValue("user-id", out var userId))
            return userId;

        return String.Empty;
    }

    public static void AddCorrelationId(this HttpRequestHeaders headers, string correlationId)
        => headers.TryAddWithoutValidation(CorrelationIdKey, correlationId);
}