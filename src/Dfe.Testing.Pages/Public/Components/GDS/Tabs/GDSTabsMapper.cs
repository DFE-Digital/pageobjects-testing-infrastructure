using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.Tabs;
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
