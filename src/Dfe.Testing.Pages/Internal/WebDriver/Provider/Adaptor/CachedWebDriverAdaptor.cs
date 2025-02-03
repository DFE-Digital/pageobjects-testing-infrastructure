using Dfe.Testing.Pages.Internal.WebDriver.Provider.Factory;

namespace Dfe.Testing.Pages.Internal.WebDriver.Provider.Adaptor;


// TODO should this be a command boundary, intake commands which can be stored to replay, observed on, logged

// NOTE Wraps WebDriver operations with lazy driver init
// NOTE IDisposable on concrete type to allow DI to Dispose() without it appearing on the interface
internal class CachedWebDriverAdaptor : IWebDriverAdaptor, IDisposable, IAsyncDisposable
{
    // TODO move factory selection and provide -  behind higher order factory
    private readonly IEnumerable<IBrowserFactory> _browserFactories;
    private readonly WebDriverOptions _webDriverOptions;
    private IWebDriver? _webDriver;

    public CachedWebDriverAdaptor(
        IEnumerable<IBrowserFactory> browserFactories,
        WebDriverOptions webDriverOptions)
    {
        ArgumentNullException.ThrowIfNull(browserFactories, nameof(browserFactories));
        _browserFactories = browserFactories;
        _webDriverOptions = webDriverOptions;
    }
    private IWebDriver Driver => _webDriver ?? throw new ArgumentNullException(nameof(_webDriver));

    public async Task StartAsync()
    {
        await StartDriverSessionAsync(_webDriverOptions);
    }

    public async Task StartAsync(Action<WebDriverOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(configureOptions);
        WebDriverOptions options = new();
        configureOptions.Invoke(options);
        await StartDriverSessionAsync(options);
    }

    private async Task StartDriverSessionAsync(WebDriverOptions options)
    {
        BrowserType normalisedType = (options.Browser.BrowserName?.ToLowerInvariant() ?? string.Empty) switch
        {
            "chrome" => BrowserType.Chrome,
            "edge" => BrowserType.Edge,
            "firefox" => BrowserType.Firefox,
            _ => BrowserType.Chrome
        };

        _webDriver = await _browserFactories.Single(t => t.Key == normalisedType).Create(options);

        if (options.Browser.EnableAuthenticationBypass)
        {
            await Driver.Manage().Network.StartMonitoring();
        }
    }

    public async Task NavigateToAsync(Uri uri)
    {
        await Driver.Navigate().GoToUrlAsync(uri);
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            using (_webDriver)
            {
                _webDriver?.Quit();
            }
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        using (_webDriver)
        {
            if (_webDriver != null)
            {
                await _webDriver.Manage().Network.StopMonitoring().ConfigureAwait(false);
            }
        }
    }


    // TODO for these operations need to queue commands
    public Task BackAsync()
    {
        throw new NotImplementedException();
    }

    public Task ReloadAsync()
    {
        throw new NotImplementedException();
    }

    public Cookie GetCookie(string cookieName)
    {
        var webDriverCookie = _webDriver!.Manage().Cookies.GetCookieNamed(cookieName);
        return new Cookie(name: webDriverCookie!.Name, value: webDriverCookie.Value);
    }

    public IEnumerable<Cookie?> GetCookies()
    {
        throw new NotImplementedException();
    }

    public Task TakeScreenshotAsync()
    {
        throw new NotImplementedException();
    }

    public IWebElement FindElement(IElementSelector selector)
        => Driver.FindElement(
            WebDriverByLocatorHelpers.CreateLocator(selector));

    public IReadOnlyCollection<IWebElement> FindElements(IElementSelector selector)
        => Driver.FindElements(
            WebDriverByLocatorHelpers.CreateLocator(selector));

    public Uri CurrentUri() => new(Driver.Url);
}
