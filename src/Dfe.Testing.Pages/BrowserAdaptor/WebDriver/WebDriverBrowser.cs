using Dfe.Testing.Pages.BrowserAdaptor.Contracts;
using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements;
using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Network;

namespace Dfe.Testing.Pages.BrowserAdaptor.WebDriver;
public sealed class WebDriverBrowser : IBrowser
{
    private readonly IWebDriver _webDriver;
    private readonly ApplicationOptions _options;

    public WebDriverBrowser(
        IWebDriver webDriver,
        ApplicationOptions applicationOptions)
    {
        ArgumentNullException.ThrowIfNull(webDriver);
        ArgumentNullException.ThrowIfNull(applicationOptions);
        _webDriver = webDriver;
        _options = applicationOptions;
        Navigate = new WebDriverNavigation(webDriver, applicationOptions);
        Network = new WebDriverNetworking(webDriver.Manage().Network);
        Elements = new WebDriverElementActions(webDriver);
    }

    public INetworking Network { get; }
    public Contracts.Navigate.INavigation Navigate { get; }
    public IElementActions Elements { get; }
}
