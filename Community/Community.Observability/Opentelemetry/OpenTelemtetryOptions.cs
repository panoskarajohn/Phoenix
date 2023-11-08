namespace Community.Observability.Logging.Options;

public class OpenTelemetryOptions
{
    public bool Enabled { get; set; }
    public string ServiceName { get; set; }
    public string Endpoint { get; set; }
}