namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Provider;
internal interface IDocumentQueryClientProvider
{
    Task<IDocumentQueryClient> CreateDocumentClientAsync(HttpRequestMessage httpRequestMessage);
}
