using Community.Attributes;
using Community.Context.Abstractions;
using Community.CQRS.Abstractions;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace Community.CQRS.Decorators;

[Decorator]
internal sealed class LoggingEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private readonly IContext _context;
    private readonly IEventHandler<T> _handler;
    private readonly ILogger<LoggingEventHandlerDecorator<T>> _logger;

    public LoggingEventHandlerDecorator(ILogger<LoggingEventHandlerDecorator<T>> logger, IContext context,
        IEventHandler<T> handler)
    {
        _logger = logger;
        _context = context;
        _handler = handler;
    }

    public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
    {
        var name = @event.GetType().Name.Underscore();
        var requestId = _context.RequestId;
        var traceId = _context.TraceId;
        var userId = _context.UserId;
        var correlationId = _context.CorrelationId;
        _logger.LogInformation(
            "Handling an event: {Name} [Request ID: {RequestId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}']...",
            name, requestId, correlationId, traceId, userId);
        await _handler.HandleAsync(@event, cancellationToken);
        _logger.LogInformation(
            "Handled an event: {Name} [Request ID: {RequestId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}']",
            name, requestId, correlationId, traceId, userId);
    }
}