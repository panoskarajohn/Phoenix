using Community.Rabbit.Abstractions.Subscriber;

namespace Community.Rabbit.Subscriber;

internal sealed class RabbitMqSubscriber : IBusSubscriber
{
    private readonly MessageSubscribersChannel _messageSubscribersChannel;

    public RabbitMqSubscriber(MessageSubscribersChannel messageSubscribersChannel)
    {
        _messageSubscribersChannel = messageSubscribersChannel;
    }

    public IBusSubscriber Subscribe<T>(Func<IServiceProvider, T, object, Task> handle)
        where T : class
    {
        var type = typeof(T);
        _messageSubscribersChannel.Writer.TryWrite(MessageSubscriber.Subscribe(type,
            (serviceProvider, message, context) => handle(serviceProvider, (T) message, context)));
        return this;
    }

    public void Dispose()
    {
    }
}