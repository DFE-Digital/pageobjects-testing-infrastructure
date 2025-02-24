using Dfe.Testing.Pages.Internal.DocumentClient.Options;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Factory;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Options;
using Dfe.Testing.Pages.Public.PageObjects;

namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.WebDriver;

internal sealed class WebDriverDocumentClientProvider : BaseDocumentClientProvider, IDocumentClientProvider
{
    private readonly IWebDriverAdaptor _webDriverAdaptor;

    public WebDriverDocumentClientProvider(
        IWebDriverAdaptor webDriverAdaptor,
        IGetTextProcessingHandlerFactory getTextProcessingHandlerFactory,
        IMapper<DocumentClientOptions, TextProcessingOptions> mapper,
        DocumentClientOptions documentClientOptions)
            : base(getTextProcessingHandlerFactory, mapper, documentClientOptions)
    {
        _webDriverAdaptor = webDriverAdaptor;
    }

    public async Task<IDocumentClient> CreateDocumentClientAsync(HttpRequestMessage httpRequestMessage)
    {
        ArgumentNullException.ThrowIfNull(httpRequestMessage, nameof(httpRequestMessage));
        ArgumentNullException.ThrowIfNull(httpRequestMessage.RequestUri, nameof(httpRequestMessage.RequestUri));
        ArgumentNullException.ThrowIfNull(_webDriverAdaptor, nameof(_webDriverAdaptor));
        // session may have already been started by the client with a configured webdriver
        await _webDriverAdaptor.StartAsync();
        await _webDriverAdaptor.NavigateToAsync(httpRequestMessage.RequestUri);
        return new WebDriverDocumentClient(
            _webDriverAdaptor,
            GetTextProcessingHandler());
    }
}
