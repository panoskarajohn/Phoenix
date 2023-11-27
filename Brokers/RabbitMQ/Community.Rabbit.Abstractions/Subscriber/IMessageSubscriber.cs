namespace Community.Rabbit.Abstractions.Subscriber;

public interface IMessageSubscriber
{
    MessageSubscriberAction Action { get; }
    Type Type { get; }
    Func<IServiceProvider, object, object, Task> Handle { get; }
}