namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Provider.WebDriver;

internal sealed class WebDriverDocumentQueryClientProvider : IDocumentQueryClientProvider
{
    private readonly IWebDriverAdaptor _webDriverAdaptor;

    public WebDriverDocumentQueryClientProvider(IWebDriverAdaptor webDriverAdaptor)
    {
        _webDriverAdaptor = webDriverAdaptor;
    }
    public async Task<IDocumentQueryClient> CreateDocumentClientAsync(HttpRequestMessage httpRequestMessage)
    {
        ArgumentNullException.ThrowIfNull(httpRequestMessage, nameof(httpRequestMessage));
        ArgumentNullException.ThrowIfNull(httpRequestMessage.RequestUri, nameof(httpRequestMessage.RequestUri));
        ArgumentNullException.ThrowIfNull(_webDriverAdaptor, nameof(_webDriverAdaptor));
        // TODO options will determine if we navigate
        await _webDriverAdaptor.NavigateToAsync(httpRequestMessage.RequestUri);
        return new WebDriverDocumentQueryClient(_webDriverAdaptor);
    }
}
