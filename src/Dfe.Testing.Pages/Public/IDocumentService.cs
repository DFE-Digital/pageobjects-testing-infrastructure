using Dfe.Testing.Pages.Internal.DocumentClient;

namespace Dfe.Testing.Pages.Public;
public interface IDocumentService
{
    // Test controls to interact with page
    Task RequestDocumentAsync(Action<IHttpRequestBuilder> configureDocumentRequest);
    Task RequestDocumentAsync(HttpRequestMessage documentRequest);

    //TODO store document into a repository so session controls aren't available in componentfactory with documentKey so pageObjectResolver can decorate the right document

    // ComponentFactory access to query document
    IEnumerable<IDocumentSection> ExecuteQuery(FindOptions query);
    void ExecuteCommand(FindOptions options, Action<IDocumentSection> command);
}
