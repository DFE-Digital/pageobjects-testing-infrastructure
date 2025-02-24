using Dfe.Testing.Pages.Contracts.Selector;

namespace Dfe.Testing.Pages.Contracts.Documents;
public sealed class FindOptions
{
    public IElementSelector? Find { get; set; }
    public IElementSelector? InScope { get; set; }
}
