namespace Community.Rabbit.Client;

public partial class RabbitMqOptions
{
    public string ConnectionName { get; set; }
    public IEnumerable<string> HostNames { get; set; }
    public int Port { get; set; }
    public string VirtualHost { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public TimeSpan RequestedHeartbeat { get; set; } = TimeSpan.FromSeconds(60);
    public TimeSpan RequestedConnectionTimeout { get; set; } = TimeSpan.FromSeconds(30);
    public TimeSpan SocketReadTimeout { get; set; } = TimeSpan.FromSeconds(30);
    public TimeSpan SocketWriteTimeout { get; set; } = TimeSpan.FromSeconds(30);
    public TimeSpan ContinuationTimeout { get; set; } = TimeSpan.FromSeconds(20);
    public TimeSpan HandshakeContinuationTimeout { get; set; } = TimeSpan.FromSeconds(10);
    public TimeSpan NetworkRecoveryInterval { get; set; } = TimeSpan.FromSeconds(5);
    public TimeSpan? MessageProcessingTimeout { get; set; }
    public ushort RequestedChannelMax { get; set; }
    public uint RequestedFrameMax { get; set; }
    public bool UseBackgroundThreadsForIO { get; set; }
    public string ConventionsCasing { get; set; }
    public int Retries { get; set; }
    public int RetryInterval { get; set; }
    public bool MessagesPersisted { get; set; }
    public ContextOptions Context { get; set; }
    public ExchangeOptions Exchange { get; set; }
    public LoggerOptions Logger { get; set; }
    public SslOptions Ssl { get; set; }
    public QueueOptions Queue { get; set; }
    public DeadLetterOptions DeadLetter { get; set; }
    public QosOptions Qos { get; set; }
    public ConventionsOptions Conventions { get; set; }
    public string SpanContextHeader { get; set; }
    public int MaxProducerChannels { get; set; }
    public bool RequeueFailedMessages { get; set; }

    public string GetSpanContextHeader()
        => string.IsNullOrWhiteSpace(SpanContextHeader) ? "span_context" : SpanContextHeader;
}