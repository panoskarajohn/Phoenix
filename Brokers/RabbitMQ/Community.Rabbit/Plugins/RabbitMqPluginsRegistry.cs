using Community.Rabbit.Abstractions.Plugins;

namespace Community.Rabbit.Plugins;

internal sealed class RabbitMqPluginsRegistry : IRabbitMqPluginsRegistry, IRabbitMqPluginsRegistryAccessor
{
    private readonly LinkedList<RabbitMqPluginChain> _plugins = new();

    public IRabbitMqPluginsRegistry Add<TPlugin>() where TPlugin : class, IRabbitMqPlugin
    {
        _plugins.AddLast(new RabbitMqPluginChain {PluginType = typeof(TPlugin)});
        return this;
    }

    LinkedList<RabbitMqPluginChain> IRabbitMqPluginsRegistryAccessor.Get()
        => _plugins;
}