using RabbitMQ.Client;

namespace Community.Rabbit.Connection;

public sealed class ProducerConnection
{
    public ProducerConnection(IConnection connection)
    {
        Connection = connection;
    }

    public IConnection Connection { get; }
}