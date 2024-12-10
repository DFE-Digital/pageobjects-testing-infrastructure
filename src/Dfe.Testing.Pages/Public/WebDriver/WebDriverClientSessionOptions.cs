namespace Dfe.Testing.Pages.Public.WebDriver;

public sealed class WebDriverClientSessionOptions
{
    // equivalent to --headless when false
    public bool ShowBrowser { get; set; } = false;
    public string BrowserName { get; set; } = string.Empty;
    public int PageLoadTimeout { get; set; } = 45;
    public int RequestTimeout { get; set; } = 60;
    public bool EnableNetworkInterception { get; set; } = false;
    public bool EnableVerboseLogging { get; set; } = false;
}
