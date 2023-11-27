using Community.CQRS.Abstractions;

namespace Artist.Core.Base;

public interface IAggregate
{
    IReadOnlyList<IEvent> DomainEvents { get; }
    void AddDomainEvent(IEvent domainEvent);
    void RemoveDomainEvent(IEvent domainEvent);
    void ClearDomainEvents();
}