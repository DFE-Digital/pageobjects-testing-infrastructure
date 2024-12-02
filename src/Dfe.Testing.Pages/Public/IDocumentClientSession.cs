namespace Dfe.Testing.Pages.Public;
public interface IDocumentClientSession
{
    Task RequestDocumentAsync(Action<IHttpRequestBuilder> configureDocumentRequest);
    Task RequestDocumentAsync(HttpRequestMessage documentRequest);
    TPage GetPage<TPage>() where TPage : class, IPage;
}
