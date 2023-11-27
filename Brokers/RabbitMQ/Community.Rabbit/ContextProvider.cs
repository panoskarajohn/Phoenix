using Community.Rabbit.Abstractions;
using Community.Rabbit.Client;

namespace Community.Rabbit;

internal sealed class ContextProvider : IContextProvider
{
    private readonly IRabbitMqSerializer _serializer;

    public ContextProvider(IRabbitMqSerializer serializer, RabbitMqOptions options)
    {
        _serializer = serializer;
        HeaderName = string.IsNullOrWhiteSpace(options.Context?.Header)
            ? "message_context"
            : options.Context.Header;
    }

    public string HeaderName { get; }

    public object Get(IDictionary<string, object> headers)
    {
        if (headers is null)
        {
            return null;
        }

        if (!headers.TryGetValue(HeaderName, out var context))
        {
            return null;
        }

        if (context is byte[] bytes)
        {
            return _serializer.Deserialize(bytes);
        }

        return null;
    }
}