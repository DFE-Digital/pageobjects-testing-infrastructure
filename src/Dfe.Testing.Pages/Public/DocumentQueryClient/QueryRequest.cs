namespace Dfe.Testing.Pages.Public.DocumentQueryClient;
public sealed class QueryRequest
{
    public IElementSelector? Query { get; set; }
    public IElementSelector? Scope { get; set; }
}
