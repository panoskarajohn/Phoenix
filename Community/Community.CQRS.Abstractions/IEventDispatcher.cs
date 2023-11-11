namespace Community.CQRS.Abstractions;

public interface IEventDispatcher
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
}