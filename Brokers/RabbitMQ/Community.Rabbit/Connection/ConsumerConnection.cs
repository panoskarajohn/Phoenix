using RabbitMQ.Client;

namespace Community.Rabbit.Connection;

public sealed class ConsumerConnection
{
    public ConsumerConnection(IConnection connection)
    {
        Connection = connection;
    }

    public IConnection Connection { get; }
}