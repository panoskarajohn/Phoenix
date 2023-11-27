using RabbitMQ.Client.Events;

namespace Community.Rabbit.Abstractions.Plugins;

public interface IRabbitMqPlugin
{
    Task HandleAsync(object message, object correlationContext, BasicDeliverEventArgs args);
}