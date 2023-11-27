using RabbitMQ.Client.Events;

namespace Community.Rabbit.Abstractions.Plugins;

public interface IRabbitMqPluginsExecutor
{
    Task ExecuteAsync(Func<object, object, BasicDeliverEventArgs, Task> successor,
        object message, object correlationContext, BasicDeliverEventArgs args);
}