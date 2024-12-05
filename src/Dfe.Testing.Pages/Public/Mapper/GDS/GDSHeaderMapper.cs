using Dfe.Testing.Pages.Components;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSHeaderMapper : IComponentMapper<GDSHeaderComponent>
{
    private readonly ComponentFactory<AnchorLinkComponent> _linkFactory;

    public GDSHeaderMapper(ComponentFactory<AnchorLinkComponent> linkFactory)
    {
        ArgumentNullException.ThrowIfNull(linkFactory);
        _linkFactory = linkFactory;
    }
    public GDSHeaderComponent Map(IDocumentPart input)
    {
        return new GDSHeaderComponent()
        {
            GovUKLink = _linkFactory.Get(new QueryOptions()
            {
                Query = new CssElementSelector(".govuk-header__link--homepage"),
            }),
            ServiceName = _linkFactory.Get(new QueryOptions()
            {
                Query = new CssElementSelector(".govuk-header__service-name"),
            }),
            TagName = input.TagName
        };
    }
}
