using System.Collections.Concurrent;
using Amazon.GuardDuty.Endpoints;
using Ardalis.GuardClauses;
using Community.Rabbit.Abstractions;
using Community.Rabbit.Abstractions.Client;
using Community.Rabbit.Abstractions.Conventions;
using Community.Rabbit.Connection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Community.Rabbit.Client;

internal sealed class RabbitMqClient : IRabbitMqClient
{
    private const string EmptyContext = "{}";
    private readonly ConcurrentDictionary<int, IModel> _channels = new();
    private readonly IConnection _connection;
    private readonly bool _contextEnabled;
    private readonly IContextProvider _contextProvider;
    private readonly object _lockObject = new();
    private readonly ILogger<RabbitMqClient> _logger;
    private readonly bool _loggerEnabled;
    private readonly int _maxChannels;
    private readonly bool _persistMessages;
    private readonly IRabbitMqSerializer _serializer;
    private readonly string _spanContextHeader;
    private int _channelsCount;

    public RabbitMqClient(ProducerConnection connection, IContextProvider contextProvider,
        IRabbitMqSerializer serializer,
        RabbitMqOptions options, ILogger<RabbitMqClient> logger)
    {
        _connection = connection.Connection;
        _contextProvider = contextProvider;
        _serializer = serializer;
        _logger = logger;
        _contextEnabled = options.Context?.Enabled == true;
        _loggerEnabled = options.Logger?.Enabled ?? false;
        _spanContextHeader = options.GetSpanContextHeader();
        _persistMessages = options?.MessagesPersisted ?? false;
        Guard.Against.NegativeOrZero(options.MaxProducerChannels, nameof(options.MaxProducerChannels));
        _maxChannels = options.MaxProducerChannels;
    }

    public void Send(object message, IConventions conventions, string messageId = null, string correlationId = null,
        string spanContext = null, object messageContext = null, IDictionary<string, object> headers = null)
    {
        var channel = GetChannel();

        var body = _serializer.Serialize(message);
        var properties = channel.CreateBasicProperties();
        properties.Persistent = _persistMessages;
        properties.MessageId = string.IsNullOrWhiteSpace(messageId)
            ? Guid.NewGuid().ToString("N")
            : messageId;
        properties.CorrelationId = string.IsNullOrWhiteSpace(correlationId)
            ? Guid.NewGuid().ToString("N")
            : correlationId;
        properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        properties.Headers = new Dictionary<string, object>();

        if (_contextEnabled)
        {
            IncludeMessageContext(messageContext, properties);
        }

        if (!string.IsNullOrWhiteSpace(spanContext))
        {
            properties.Headers.Add(_spanContextHeader, spanContext);
        }

        if (headers is not null)
        {
            foreach (var (key, value) in headers)
            {
                if (string.IsNullOrWhiteSpace(key) || value is null)
                {
                    continue;
                }

                properties.Headers.TryAdd(key, value);
            }
        }

        if (_loggerEnabled)
        {
            _logger.LogInformation($"Publishing a message with routing key: '{conventions.RoutingKey}' " +
                             $"to exchange: '{conventions.Exchange}' " +
                             $"[id: '{properties.MessageId}', correlation id: '{properties.CorrelationId}']");
        }

        channel.BasicPublish(conventions.Exchange, conventions.RoutingKey, properties, body.ToArray());
    }

    private IModel GetChannel()
    {
        var threadId = Thread.CurrentThread.ManagedThreadId;
        if (!_channels.TryGetValue(threadId, out var channel))
        {
            lock (_lockObject)
            {
                if (_channelsCount >= _maxChannels)
                {
                    throw new InvalidOperationException(
                        $"Cannot create RabbitMQ producer channel for thread: {threadId} " +
                        $"(reached the limit of {_maxChannels} channels). " +
                        "Modify `MaxProducerChannels` setting to allow more channels.");
                }

                channel = _connection.CreateModel();
                _channels.TryAdd(threadId, channel);
                _channelsCount++;
                if (_loggerEnabled)
                {
                    _logger.LogTrace(
                        $"Created a channel for thread: {threadId}, total channels: {_channelsCount}/{_maxChannels}");
                }
            }
        }
        else
        {
            if (_loggerEnabled)
            {
                _logger.LogTrace(
                    $"Reused a channel for thread: {threadId}, total channels: {_channelsCount}/{_maxChannels}");
            }
        }

        return channel;
    }

    private void IncludeMessageContext(object context, IBasicProperties properties)
    {
        if (context is not null)
        {
            properties.Headers.Add(_contextProvider.HeaderName, _serializer.Serialize(context).ToArray());
            return;
        }

        properties.Headers.Add(_contextProvider.HeaderName, EmptyContext);
    }
}