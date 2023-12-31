namespace Community.Rabbit.Abstractions.Publisher;

public interface IBusPublisher
{
    Task PublishAsync<T>(T message, string messageId = null, string correlationId = null, string spanContext = null,
        object messageContext = null, IDictionary<string, object> headers = null) where T : class;
}