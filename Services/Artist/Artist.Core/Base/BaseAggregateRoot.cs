using Community.CQRS.Abstractions;

namespace Artist.Core.Base;

public abstract class BaseAggregateRoot<TId> : Entity<TId>, IAggregate
{
    private readonly List<IEvent> _domainEvents = new();

    public IReadOnlyList<IEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IEvent domainEvent)
    {
        if (!_domainEvents.Any())
        {
            Version++;
        }

        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IEvent domainEvent)
        => _domainEvents?.Remove(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents?.Clear();
}