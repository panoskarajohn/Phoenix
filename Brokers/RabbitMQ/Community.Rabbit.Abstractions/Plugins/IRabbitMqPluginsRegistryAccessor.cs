namespace Community.Rabbit.Abstractions.Plugins;

public interface IRabbitMqPluginsRegistryAccessor
{
    LinkedList<RabbitMqPluginChain> Get();
}