namespace Community.Rabbit.Abstractions.Message;

public interface IMessageProperties
{
    string MessageId { get; }
    string CorrelationId { get; }
    long Timestamp { get; }
    IDictionary<string, object> Headers { get; }
}