namespace Dfe.Testing.Pages.Internal.WebDriver.Provider;
/*internal sealed class CachedWebDriverAdaptorProvider : IWebDriverAdaptorProvider
{


    public CachedWebDriverAdaptorProvider(
        WebDriverClientSessionOptions webDriverClientSessionOptions,
        IWebDriverSessionOptionsBuilder webDriverSessionOptionsBuilder)
    {
        ArgumentNullException.ThrowIfNull(webDriverClientSessionOptions);
        ArgumentNullException.ThrowIfNull(webDriverSessionOptionsBuilder);
    }

    // TODO decorate core WebDriverAdaptorProvider with CachedWebDriverAdaptorProvider to separate concerns
    public async Task<IWebDriverAdaptor> GetAsync()
    {
        if (_cachedWebDriverInstance == null)
        {
            try
            {
                // TODO switch on options.BrowserType to choose which concrete WebDriverFactory
                var factory = new ChromeDriverFactory();
                await _semaphore.WaitAsync();
                // TODO browser options Dictionary
                // TODO browser version
                var sessionOptions = _webDriverSessionOptionsBuilder
                    .WithBrowserType(_webDriverClientSessionOptions.BrowserName)
                    .WithNetworkInterception(_webDriverClientSessionOptions.EnableNetworkInterception)
                    .WithPageLoadTimeout(_webDriverClientSessionOptions.PageLoadTimeout)
                    .WithRequestTimeout(_webDriverClientSessionOptions.RequestTimeout)
                    .Build();

                // TODO eager initialisation switch from clientSessionOptions
                _cachedWebDriverInstance = new LazyWebDriverAdaptor(
                    getDriver: await factory.CreateDriver(sessionOptions),
                    sessionOptions);
            }
            finally
            {
                _semaphore.Release();
            }
        }
        return _cachedWebDriverInstance;
    }
}
*/
