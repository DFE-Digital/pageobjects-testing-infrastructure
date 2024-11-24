namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;

internal sealed class PageFactory : IPageFactory
{
    private readonly IEnumerable<IPage> _pages;
    private readonly IDocumentQueryClientAccessor _documentQueryClientAccessor;
    private readonly IDocumentQueryClientProvider _documentQueryClientProvider;

    public PageFactory(
        IEnumerable<IPage> pages,
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IDocumentQueryClientProvider documentClientProvider)
    {
        ArgumentNullException.ThrowIfNull(documentClientProvider);
        ArgumentNullException.ThrowIfNull(documentQueryClientAccessor);
        _pages = pages;
        _documentQueryClientAccessor = documentQueryClientAccessor;
        _documentQueryClientProvider = documentClientProvider;
    }

    public async Task<TPage> CreatePageAsync<TPage>(HttpRequestMessage httpRequestMessage) where TPage : class, IPage
    {
        IDocumentQueryClient documentClient = await _documentQueryClientProvider.CreateDocumentClientAsync(httpRequestMessage)
            ?? throw new InvalidOperationException("Document client is null.");

        // componentFactories need to to resolve the created IDocumentQueryClient through proxy object IDocumentQueryClientAccessor in the same scope
        _documentQueryClientAccessor.DocumentQueryClient = documentClient;
        return ResolvePage<TPage>();
    }

    private TPage ResolvePage<TPage>() where TPage : class, IPage
        => (TPage)_pages.Single(
                (page) => page.GetType().Name == typeof(TPage).Name);
}
