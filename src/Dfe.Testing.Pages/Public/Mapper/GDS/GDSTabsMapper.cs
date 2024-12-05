using Dfe.Testing.Pages.Components;
using Dfe.Testing.Pages.Components.Tabs;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSTabsMapper : IComponentMapper<GDSTabsComponent>
{
    private readonly ComponentFactory<AnchorLinkComponent> _anchorLinkFactory;

    public GDSTabsMapper(ComponentFactory<AnchorLinkComponent> anchorLinkFactory)
    {
        ArgumentNullException.ThrowIfNull(anchorLinkFactory);
        _anchorLinkFactory = anchorLinkFactory;
    }
    public GDSTabsComponent Map(IDocumentPart input)
    {
        return new GDSTabsComponent()
        {
            Heading = input.FindDescendant(new CssElementSelector(".govuk-tabs__title"))?.Text ?? string.Empty,
            Tabs = _anchorLinkFactory.GetMany(new QueryOptions()
            {
                InScope = new CssElementSelector(".govuk-tabs__list")
            }),
            TagName = input.TagName ?? string.Empty
        };
    }
}
