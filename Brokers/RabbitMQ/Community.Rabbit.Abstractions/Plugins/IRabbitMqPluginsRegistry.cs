namespace Community.Rabbit.Abstractions.Plugins;

public interface IRabbitMqPluginsRegistry
{
    IRabbitMqPluginsRegistry Add<TPlugin>() where TPlugin : class, IRabbitMqPlugin;
}