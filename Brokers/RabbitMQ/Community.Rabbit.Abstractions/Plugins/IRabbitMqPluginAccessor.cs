using RabbitMQ.Client.Events;

namespace Community.Rabbit.Abstractions.Plugins;

public interface IRabbitMqPluginAccessor
{
    void SetSuccessor(Func<object, object, BasicDeliverEventArgs, Task> successor);
}