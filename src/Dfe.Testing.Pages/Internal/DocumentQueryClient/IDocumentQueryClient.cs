using Dfe.Testing.Pages.Public.Selector.Options;

namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;
internal interface IDocumentQueryClient
{
    void Run(QueryOptions args, Action<IDocumentPart> handler);
    IDocumentPart Query(QueryOptions args);
    IEnumerable<IDocumentPart> QueryMany(QueryOptions args);
}
