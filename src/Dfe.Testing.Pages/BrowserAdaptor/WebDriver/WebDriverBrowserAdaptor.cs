using Dfe.Testing.Pages.BrowserAdaptor.Contracts;
using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Internal;

namespace Dfe.Testing.Pages.BrowserAdaptor.WebDriver;
// type made public to allow decorating
public sealed class WebDriverBrowserAdaptor : IBrowserAdaptor, IDisposable, IAsyncDisposable
{
    private IWebDriver? _webDriver = null;

    public async Task<IBrowser> StartSessionAsync(BrowserStartSessionRequest? options) => await StartSessionInternalAsync(options);
    public async Task<IBrowser> StartSessionAsync(Action<BrowserStartSessionRequest> configureOptions)
    {
        BrowserStartSessionRequest options = new();
        configureOptions?.Invoke(options);
        return await StartSessionInternalAsync(options);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _webDriver?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_webDriver is IAsyncDisposable webDriverAsyncDisposable)
        {
            await _webDriver.Manage().Network.StopMonitoring();
            await webDriverAsyncDisposable.DisposeAsync();
        }
        else
        {
            _webDriver?.Dispose();
        }
    }

    private async Task<IBrowser> StartSessionInternalAsync(BrowserStartSessionRequest? options = null)
    {
        BrowserStartSessionRequest normalisedOptions = options ?? new();

        // TODO future support multiple browser sessions with driverSessionKey than stored as state in adaptor
        if (_webDriver is not null)
        {
            throw new InvalidOperationException("Browser session already started");
        }

        _webDriver = await CreateDriverAsync(normalisedOptions);
        return new WebDriverBrowser(_webDriver!, normalisedOptions.Application);
    }

    private static async Task<IWebDriver> CreateDriverAsync(BrowserStartSessionRequest options)
    {
        if (options.Browser.Type == BrowserType.Chrome)
        {
            ChromeWebDriverFactory chromeDriverFactory = new();
            return await chromeDriverFactory.CreateDriverAsync(options);

        }
        throw new ArgumentException($"Unable to create BrowserType {options.Browser.Type}");
    }
}


