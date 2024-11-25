using Dfe.Testing.Pages.Components.AnchorLink;
using Dfe.Testing.Pages.Components.Header;
using Dfe.Testing.Pages.Internal.Components;

namespace Dfe.Testing.Pages.Internal.Components.Header;
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
            GovUKLink = _linkFactory.Get(new QueryRequestArgs()
            {
                Query = new CssSelector(".govuk-header__link--homepage"),
            }),
            ServiceName = _linkFactory.Get(new QueryRequestArgs()
            {
                Query = new CssSelector(".govuk-header__service-name"),
            }),
            TagName = input.TagName
        };
    }
}
