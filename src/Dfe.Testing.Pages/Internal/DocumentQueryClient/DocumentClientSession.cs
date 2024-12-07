using Dfe.Testing.Pages.Internal.DocumentQueryClient.Resolver;

namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;
internal sealed class DocumentSessionClient : IDocumentSessionClient
{
    private IDocumentQueryClient? _documentQueryClient;
    private readonly IDocumentQueryClientAccessor _accessor;
    private readonly IDocumentQueryClientProvider _provider;
    private readonly IHttpRequestBuilder _requestBuilder;
    private readonly IPageObjectResolver _pageResolver;

    public DocumentSessionClient(
        IDocumentQueryClientProvider provider,
        IDocumentQueryClientAccessor accessor,
        IHttpRequestBuilder requestBuilder,
        IPageObjectResolver pageResolver)
    {
        _provider = provider;
        _accessor = accessor;
        _requestBuilder = requestBuilder;
        _pageResolver = pageResolver;
    }

    public Task RequestDocumentAsync(Action<IHttpRequestBuilder> builder)
    {
        builder?.Invoke(_requestBuilder);
        var message = _requestBuilder.Build();
        return RequestDocumentAsync(message);
    }

    public async Task RequestDocumentAsync(HttpRequestMessage request)
    {
        _documentQueryClient =
            await _provider.CreateDocumentClientAsync(request)
                ?? throw new InvalidOperationException("Document client is null.");

        // TEMP accessor needs to be set as used in ComponentFactory<T> to resolve the created IDocumentQueryClient
        _accessor.DocumentQueryClient = _documentQueryClient;
    }

    public TPage GetPageObject<TPage>() where TPage : class, IPageObject
    {
        EnsureDocumentCreated();
        return _pageResolver.GetPage<TPage>();
    }

    private void EnsureDocumentCreated()
    {
        if (_documentQueryClient == null)
        {
            throw new ArgumentException($"Document has not been created yet. Call {nameof(RequestDocumentAsync)} before attempting to create pages");
        }
    }
}

