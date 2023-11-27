using Community.CQRS.Abstractions;
using Community.Rabbit.Abstractions.Correlation;
using Community.Rabbit.Abstractions.Publisher;

namespace Community.Rabbit.Cqrs.EventSubscriber;

internal sealed class ServiceBusMessageDispatcher : ICommandDispatcher, IEventDispatcher
{
    private readonly ICorrelationContextAccessor _accessor;
    private readonly IBusPublisher _busPublisher;

    public ServiceBusMessageDispatcher(IBusPublisher busPublisher, ICorrelationContextAccessor accessor)
    {
        _busPublisher = busPublisher;
        _accessor = accessor;
    }

    public Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand
        => _busPublisher.SendAsync(command, _accessor.CorrelationContext);

    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
        => _busPublisher.PublishAsync(@event, _accessor.CorrelationContext);
}