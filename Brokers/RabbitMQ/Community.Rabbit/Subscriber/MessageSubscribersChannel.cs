using System.Threading.Channels;
using Community.Rabbit.Abstractions.Subscriber;

namespace Community.Rabbit.Subscriber;

internal class MessageSubscribersChannel
{
    private readonly Channel<IMessageSubscriber> _channel = Channel.CreateUnbounded<IMessageSubscriber>();

    public ChannelReader<IMessageSubscriber> Reader => _channel.Reader;
    public ChannelWriter<IMessageSubscriber> Writer => _channel.Writer;
}