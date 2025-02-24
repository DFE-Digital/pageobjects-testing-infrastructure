namespace Dfe.Testing.Pages.Public.PageObjects.Documents;
public interface IDocumentService
{
    //TODO consider returning the IDocumentClient or a DocumentReadOnlyModel?
    Task RequestDocumentAsync(Action<IHttpRequestBuilder> configureDocumentRequest);
    Task RequestDocumentAsync(HttpRequestMessage documentRequest);

    // TODO store document into a repository passing optional documentKey to persist the document
    // TODO make ComponentFactory depend only on DocumentRepository so it builds components from Documents, removing it from this interface and the client.
    IEnumerable<IDocumentSection> ExecuteQuery(FindOptions query);
    void ExecuteCommand(FindOptions options, Action<IDocumentSection> command);
}
