using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Internal.DocumentClient;
internal sealed class DocumentSectionFinder : IDocumentSectionFinder
{
    private readonly IComponentSelectorFactory _componentSelectorFactory;

    public DocumentSectionFinder(IComponentSelectorFactory componentSelectorFactory)
    {
        _componentSelectorFactory = componentSelectorFactory;
    }

    public IDocumentSection Find<TComponent>(IDocumentSection section) where TComponent : class => FindMany<TComponent>(section).SingleOrDefault()!;

    public IDocumentSection Find(IDocumentSection section, IElementSelector selector) => FindMany(section, selector).SingleOrDefault()!;

    public IEnumerable<IDocumentSection> FindMany<TComponent>(IDocumentSection section) where TComponent : class
    {
        var selector = _componentSelectorFactory.GetSelector<TComponent>();
        return FindMany(section, selector);
    }

    public IEnumerable<IDocumentSection> FindMany(IDocumentSection section, IElementSelector selector)
    {
        ArgumentNullException.ThrowIfNull(section);
        ArgumentNullException.ThrowIfNull(selector);
        var descendants = section.FindDescendants(selector);
        return descendants ?? [];
    }
}
