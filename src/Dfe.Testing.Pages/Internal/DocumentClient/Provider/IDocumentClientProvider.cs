namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider;
internal interface IDocumentClientProvider
{
    Task<IDocumentClient> CreateDocumentClientAsync(HttpRequestMessage httpRequestMessage);
}
