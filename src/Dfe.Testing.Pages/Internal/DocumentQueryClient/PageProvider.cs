namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;

internal sealed class PageProvider : IPageProvider
{
    private readonly IPageFactory _pageFactory;
    private readonly IDocumentQueryClientAccessor _documentQueryClientAccessor;
    private readonly IDocumentQueryClientProvider _documentQueryClientProvider;

    public PageProvider(
        IPageFactory pageFactory,
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IDocumentQueryClientProvider documentClientProvider)
    {
        ArgumentNullException.ThrowIfNull(documentClientProvider);
        _pageFactory = pageFactory;
        _documentQueryClientAccessor = documentQueryClientAccessor;
        _documentQueryClientProvider = documentClientProvider;
    }

    public async Task<PageBase> CreatePageAsync<TPage>(HttpRequestMessage httpRequestMessage) where TPage : PageBase
    {
        IDocumentQueryClient documentClient = (await _documentQueryClientProvider.CreateDocumentClientAsync(httpRequestMessage)) ?? throw new ArgumentNullException("Document client is null.");

        // components need to be able to resolve the created IDocumentQueryClient in the same scope
        _documentQueryClientAccessor.DocumentQueryClient = documentClient;
        return _pageFactory.CreatePage<TPage>();
    }
}
