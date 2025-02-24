using Dfe.Testing.Pages.Public.PageObjects.Documents;

namespace Dfe.Testing.Pages.Internal.DocumentClient;
internal interface IDocumentClient
{
    void Run(FindOptions args, Action<IDocumentSection> handler);
    IDocumentSection Query(FindOptions args);
    IEnumerable<IDocumentSection> QueryMany(FindOptions args);
}
