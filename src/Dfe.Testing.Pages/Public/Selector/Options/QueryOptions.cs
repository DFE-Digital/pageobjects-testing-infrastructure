using Dfe.Testing.Pages.Public.Selector.Factory;

namespace Dfe.Testing.Pages.Public.Selector.Options;
public sealed class QueryOptions
{
    public IElementSelector? Query { get; set; }
    public IElementSelector? InScope { get; set; }
}
