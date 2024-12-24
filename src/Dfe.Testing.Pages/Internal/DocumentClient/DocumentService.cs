using Dfe.Testing.Pages.Internal.DocumentClient.Provider;

namespace Dfe.Testing.Pages.Internal.DocumentClient;
internal sealed class DocumentService : IDocumentService
{
    private IDocumentClient? _documentClient;
    private readonly IDocumentClientProvider _provider;
    private readonly IHttpRequestBuilder _requestBuilder;

    public DocumentService(
        IDocumentClientProvider provider,
        IHttpRequestBuilder requestBuilder)
    {
        ArgumentNullException.ThrowIfNull(nameof(provider));
        ArgumentNullException.ThrowIfNull(nameof(requestBuilder));
        _provider = provider;
        _requestBuilder = requestBuilder;
    }

    public async Task RequestDocumentAsync(Action<IHttpRequestBuilder> builder)
    {
        builder?.Invoke(_requestBuilder);
        var message = _requestBuilder.Build();
        await RequestDocumentAsync(message);
    }

    public async Task RequestDocumentAsync(HttpRequestMessage request)
    {
        _documentClient =
            await _provider.CreateDocumentClientAsync(request)
                ?? throw new InvalidOperationException("Document client is null.");
    }

    public IEnumerable<IDocumentSection> ExecuteQuery(FindOptions findOptions)
    {
        EnsureDocumentCreated();
        return _documentClient!.QueryMany(findOptions).ToList();
    }

    public void ExecuteCommand(FindOptions options, Action<IDocumentSection> command)
    {
        EnsureDocumentCreated();
        _documentClient!.Run(options, command);
    }

    private void EnsureDocumentCreated()
    {
        if (_documentClient == null)
        {
            throw new ArgumentException($"Document has not been created yet. Call {nameof(RequestDocumentAsync)} before attempting to create pages");
        }
    }
}
