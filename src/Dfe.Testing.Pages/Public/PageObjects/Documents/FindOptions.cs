using Dfe.Testing.Pages.Public.PageObjects.Selector;

namespace Dfe.Testing.Pages.Public.PageObjects.Documents;
public sealed class FindOptions
{
    public IElementSelector? Find { get; set; }
    public IElementSelector? InScope { get; set; }
}
