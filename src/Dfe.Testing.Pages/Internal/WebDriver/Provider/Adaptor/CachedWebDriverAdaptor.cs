using Dfe.Testing.Pages.Internal.WebDriver.Provider.Adaptor.Factory;

namespace Dfe.Testing.Pages.Internal.WebDriver.Provider.Adaptor;

// Wraps WebDriver operations with lazy init
// TODO should this be a command boundary, intake commands which can be stored to replay, observed on, logged

// IDisposable on concrete type to allow DI to manage Dispose() without exposing it on the interface
internal class CachedWebDriverAdaptor : IWebDriverAdaptor, IDisposable, IAsyncDisposable
{
    // TODO 
    //private static readonly SemaphoreSlim _semaphore = new(1, 1);
    // TODO move factory selection and provide -  behind higher order factory
    private readonly IEnumerable<IBrowserFactory> _browserFactories;
    private readonly WebDriverClientSessionOptions _webDriverClientSessionOptions;
    private readonly IWebDriverSessionOptionsBuilder _webDriverSessionOptionsBuilder;
    private IWebDriver? _webDriver;

    public CachedWebDriverAdaptor(
        IEnumerable<IBrowserFactory> browserFactories,
        WebDriverClientSessionOptions webDriverClientSessionOptions,
        IWebDriverSessionOptionsBuilder webDriverSessionOptionsBuilder)
    {
        ArgumentNullException.ThrowIfNull(browserFactories, nameof(browserFactories));
        ArgumentNullException.ThrowIfNull(webDriverClientSessionOptions, nameof(webDriverClientSessionOptions));
        _browserFactories = browserFactories;
        _webDriverClientSessionOptions = webDriverClientSessionOptions;
        _webDriverSessionOptionsBuilder = webDriverSessionOptionsBuilder;
    }
    private IWebDriver Driver => _webDriver ?? throw new ArgumentNullException(nameof(_webDriver));

    public async Task StartAsync()
    {
        // Use the factory with mapper putting in the default client options
        WebDriverSessionOptions sessionOptions = _webDriverSessionOptionsBuilder
            .WithBrowserType(_webDriverClientSessionOptions.BrowserName)
            .WithNetworkInterception(_webDriverClientSessionOptions.EnableNetworkInterception)
            .WithPageLoadTimeout(_webDriverClientSessionOptions.PageLoadTimeout)
            .WithRequestTimeout(_webDriverClientSessionOptions.RequestTimeout)
            .Build();

        _webDriver = await _browserFactories.Single(t => t.Key == sessionOptions.BrowserType).Create(sessionOptions);
        if (_webDriverClientSessionOptions.EnableNetworkInterception)
        {
            await Driver.Manage().Network.StartMonitoring();
        }
    }

    public async Task StartAsync(Action<WebDriverClientSessionOptions> configureSessionOptions)
    {
        // Use the factory with mapper putting in overriden client options
        await Task.CompletedTask;

    }

    public async Task NavigateToAsync(Uri uri) => await Driver.Navigate().GoToUrlAsync(uri);

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
            using (Driver)
            {
                Driver?.Quit();
            }
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        using (Driver)
        {
            await Driver.Manage().Network.StopMonitoring().ConfigureAwait(false);
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
        return new Cookie(name: webDriverCookie.Name, value: webDriverCookie.Value);
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
