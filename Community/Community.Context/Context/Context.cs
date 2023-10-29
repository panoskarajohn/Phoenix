using Microsoft.AspNetCore.Http;

namespace Community.Context.Context;

internal class Context : IContext
{
    public Guid RequestId { get; } = Guid.NewGuid();
    public Guid CorrelationId { get; }
    public string TraceId { get; }
    public string IpAddress { get; }
    public string UserAgent { get; }
    public string UserId { get; set; }

    public Context() : this(Guid.NewGuid(), $"{Guid.NewGuid():N}", null)
    {
    }

    public Context(HttpContext context) : this(context.TryGetCorrelationId(), context.TraceIdentifier,
        context.TryGetUserId(), context.GetUserIpAddress(),
        context.Request.Headers["user-agent"])
    {
    }

    public Context(Guid? correlationId, string traceId, string? userId = null, string? ipAddress = null,
        string? userAgent = null)
    {
        CorrelationId = correlationId ?? Guid.NewGuid();
        TraceId = traceId;
        UserId = userId ?? string.Empty;
        IpAddress = ipAddress ?? string.Empty;
        UserAgent = userAgent ?? string.Empty;
    }

    public static IContext Empty => new Context();
}