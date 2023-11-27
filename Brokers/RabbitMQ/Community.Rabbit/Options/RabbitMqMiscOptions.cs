namespace Community.Rabbit.Client;

public partial class RabbitMqOptions
{
    public class LoggerOptions
    {
        public bool Enabled { get; set; }
        public bool LogConnectionStatus { get; set; }
        public bool LogMessagePayload { get; set; }
    }
    
    public class ContextOptions
    {
        public bool Enabled { get; set; }
        public string Header { get; set; }
    }

    public class ExchangeOptions
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Declare { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }
    }

    public class QueueOptions
    {
        public string Template { get; set; }
        public bool Declare { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
    }

    public class DeadLetterOptions
    {
        public bool Enabled { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public bool Declare { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public int? Ttl { get; set; }
    }

    public class SslOptions
    {
        public bool Enabled { get; set; }
        public string ServerName { get; set; }
        public string CertificatePath { get; set; }
        public string CaCertificatePath { get; set; }
        public IEnumerable<string> X509IgnoredStatuses { get; set; }
    }

    public class QosOptions
    {
        public uint PrefetchSize { get; set; }
        public ushort PrefetchCount { get; set; }
        public bool Global { get; set; }
    }

    public class ConventionsOptions
    {
        public MessageAttributeOptions MessageAttribute { get; set; }

        public class MessageAttributeOptions
        {
            public bool IgnoreExchange { get; set; }
            public bool IgnoreRoutingKey { get; set; }
            public bool IgnoreQueue { get; set; }
        }
    }
}
