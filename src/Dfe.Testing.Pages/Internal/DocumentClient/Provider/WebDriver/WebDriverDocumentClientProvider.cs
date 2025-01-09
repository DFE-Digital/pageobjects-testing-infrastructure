using Dfe.Testing.Pages.Internal.DocumentClient.Options;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Factory;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Options;

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
        // TODO options will determine if we navigate
        await _webDriverAdaptor.NavigateToAsync(httpRequestMessage.RequestUri);
        return new WebDriverDocumentClient(
            _webDriverAdaptor,
            GetTextProcessingHandler());
    }
}
