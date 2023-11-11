namespace Community.CQRS.Abstractions;

public interface IRejectedEvent : IEvent
{
    string Reason { get; }
    string Code { get; }
}