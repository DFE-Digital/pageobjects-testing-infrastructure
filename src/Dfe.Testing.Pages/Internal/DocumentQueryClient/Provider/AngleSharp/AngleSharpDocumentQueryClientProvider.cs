namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Provider.AngleSharp;
internal class AngleSharpDocumentQueryClientProvider : IDocumentQueryClientProvider
{
    private readonly IHtmlDocumentProvider _documentProvider;

    public AngleSharpDocumentQueryClientProvider(IHtmlDocumentProvider documentProvider)
    {
        _documentProvider = documentProvider;
    }
    public async Task<IDocumentQueryClient> CreateDocumentClientAsync(HttpRequestMessage httpRequestMessage)
    {
        IHtmlDocument document = await _documentProvider.Get(httpRequestMessage);
        return new AngleSharpDocumentQueryClient(document);
    }
}
