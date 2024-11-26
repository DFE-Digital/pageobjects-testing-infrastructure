using Dfe.Testing.Pages.Components.Tabs;
using Dfe.Testing.Pages.Public.Mapper.Interface;

namespace Dfe.Testing.Pages.Public.Mapper;
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
            Heading = input.GetChild(new CssSelector(".govuk-tabs__title"))?.Text ?? string.Empty,
            Tabs = _anchorLinkFactory.GetMany(new QueryRequestArgs()
            {
                Scope = new CssSelector(".govuk-tabs__list")
            }),
            TagName = input.TagName ?? string.Empty
        };
    }
}
