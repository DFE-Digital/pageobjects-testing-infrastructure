namespace Dfe.Testing.Pages.Internal.DocumentClient;
internal interface IDocumentClient
{
    void Run(FindOptions args, Action<IDocumentSection> handler);
    IDocumentSection Query(FindOptions args);
    IEnumerable<IDocumentSection> QueryMany(FindOptions args);
}

public sealed class FindOptions
{
    public IElementSelector? Find { get; set; }
    public IElementSelector? InScope { get; set; }
}
