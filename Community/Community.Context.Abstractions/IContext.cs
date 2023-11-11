namespace Community.Context.Abstractions;

public interface IContext
{
    Guid RequestId { get; }
    Guid CorrelationId { get; }
    string TraceId { get; }
    string IpAddress { get; }
    string UserAgent { get; }
    string UserId { get; }
}