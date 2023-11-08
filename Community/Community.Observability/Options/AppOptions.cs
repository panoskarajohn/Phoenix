namespace Community.Observability.Options;

public class AppOptions
{
    public string Name { get; set; } = String.Empty;
    public string Service { get; set; } = string.Empty;
    public string Instance { get; set; } = String.Empty;
    public string Version { get; set; } = String.Empty;
    public bool DisplayBanner { get; set; } = true;
    public bool DisplayVersion { get; set; } = true;
}