namespace Dfe.Testing.Pages.Public;
public interface IDocumentClientSession
{
    Task RequestDocumentAsync(Action<IHttpRequestBuilder> configureDocumentRequest);
    Task RequestDocumentAsync(HttpRequestMessage documentRequest);
    TPage GetPageObject<TPage>() where TPage : class, IPageObject;
}
