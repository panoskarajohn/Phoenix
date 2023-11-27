namespace Community.Rabbit.Abstractions.Subscriber;

public interface IBusSubscriber : IDisposable
{
    IBusSubscriber Subscribe<T>(Func<IServiceProvider, T, object, Task> handle) where T : class;
}