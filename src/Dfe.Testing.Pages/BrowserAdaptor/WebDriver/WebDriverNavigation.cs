using Dfe.Testing.Pages.BrowserAdaptor.Contracts;

namespace Dfe.Testing.Pages.BrowserAdaptor.WebDriver;

internal class WebDriverNavigation : Contracts.Navigate.INavigation
{
    private readonly IWebDriver _webDriver;
    private readonly ApplicationOptions _applicationOptions;

    public WebDriverNavigation(
        IWebDriver webDriver,
        ApplicationOptions applicationOptions)
    {
        ArgumentNullException.ThrowIfNull(webDriver);
        ArgumentNullException.ThrowIfNull(applicationOptions);
        _webDriver = webDriver;
        _applicationOptions = applicationOptions;
    }

    public Uri CurrentUri => new(_webDriver.Url, UriKind.RelativeOrAbsolute);

    public async Task NavigateToUriAsync(string requestUri, CancellationToken ctx = default)
    {
        ArgumentNullException.ThrowIfNull(requestUri);

        // absolute path http:// or https://
        if (requestUri.StartsWith("http") && Uri.TryCreate(requestUri, UriKind.Absolute, out Uri? absoluteUriRequested))
        {
            await NavigateToUriAsync(absoluteUriRequested, ctx);
            return;
        }

        // relative path "/path-to-page"
        if (requestUri.StartsWith('/') && Uri.TryCreate($"{_applicationOptions.Uri}{requestUri}", UriKind.Absolute, out Uri? relativeUriRequested))
        {
            await NavigateToUriAsync(relativeUriRequested, ctx);
            return;
        }
        // assume it's a relative path to application address
        if (!Uri.TryCreate($"{_applicationOptions.Uri}/{requestUri}", UriKind.Absolute, out Uri? relativeConstructedPathRequested))
        {
            throw new ArgumentException($"Unable to construct a valid Uri from {requestUri}");
        }

        await NavigateToUriAsync(relativeConstructedPathRequested, ctx);
    }


    public async Task NavigateToUriAsync(Uri requestUri, CancellationToken ctx = default)
    {
        ArgumentNullException.ThrowIfNull(requestUri);
        await _webDriver.Navigate().GoToUrlAsync(requestUri); // note : no overload to pass ctx.
    }
}
