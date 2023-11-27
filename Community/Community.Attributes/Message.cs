namespace Community.Attributes;

/// <summary>
/// Used to define Rabbit MQ msgs
/// Maybe we can reuse this for other Brokers but not yet verified
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RabbitMessageAttribute : Attribute
{
    public RabbitMessageAttribute(string exchange = null, 
        string routingKey = null,
        string exchangeType = "topic",
        string queue = null,
        bool external = false)
    {
        Exchange = exchange;
        RoutingKey = routingKey;
        Queue = queue;
        External = external;
        Type = exchangeType;
    }

    public string Exchange { get; }
    public string RoutingKey { get; }
    public string Type { get; }
    public string Queue { get; }
    public bool External { get; }
}