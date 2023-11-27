using System.Reflection;
using Community.Attributes;
using Community.Rabbit.Abstractions.Conventions;
using Community.Rabbit.Client;
using Humanizer;

namespace Community.Rabbit.Conventions;

public class ConventionsBuilder : IConventionsBuilder
{
    private readonly RabbitMqOptions _options;
    private readonly string _queueTemplate;

    public ConventionsBuilder(RabbitMqOptions options)
    {
        _options = options;
        _queueTemplate = "{{assembly}}/{{exchange}}.{{message}}";
    }

    public string GetRoutingKey(Type type)
    {
        var attribute = GeAttribute(type);
        string routingKey = attribute.RoutingKey;
        return routingKey.Underscore();
    }

    public string GetExchange(Type type)
    {
        var exchange = string.IsNullOrWhiteSpace(_options.Exchange?.Name)
            ? type.Assembly.GetName().Name
            : _options.Exchange.Name;
        var attribute = GeAttribute(type);
        exchange = string.IsNullOrWhiteSpace(attribute?.Exchange) ? exchange : attribute.Exchange;

        return exchange.Underscore();
    }

    public string GetQueue(Type type)
    {
        var attribute = GeAttribute(type);
        var assembly = type.Assembly.GetName().Name;
        var message = type.Name;
        var exchange = string.IsNullOrWhiteSpace(attribute?.Exchange)
            ? _options.Exchange?.Name
            : attribute.Exchange;
        var queue = _queueTemplate.Replace("{{assembly}}", assembly)
            .Replace("{{exchange}}", exchange)
            .Replace("{{message}}", message);

        return queue.Underscore();
    }

    private static RabbitMessageAttribute GeAttribute(MemberInfo type) => type.GetCustomAttribute<RabbitMessageAttribute>()!;
}