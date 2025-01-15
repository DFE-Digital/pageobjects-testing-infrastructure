namespace Dfe.Testing.Pages.Public.WebDriver.Options;
public sealed class BrowserOptions
{
    public string BrowserName { get; set; } = "chrome";
    public string BrowserVersionMajor { get; set; } = string.Empty; // e.g chrome 121. empty string uses system latest supported driver
    public bool ShowBrowser { get; set; } = true; // headless
    public int PageLoadTimeoutSeconds { get; set; } = 45;
    public int ViewportWidth { get; set; } = 1920;
    public int ViewportHeight { get; set; } = 1080;
    public IList<string> CustomOptions { get; set; } = [];
    public bool EnableIncongnito { get; set; } = true;
    public bool EnableAuthenticationBypass { get; set; } = false;
    //public bool EnableJavascript { get; set; } = true;
}
