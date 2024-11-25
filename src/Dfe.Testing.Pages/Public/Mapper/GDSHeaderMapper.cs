using Dfe.Testing.Pages.Public.Mapper.Interface;

namespace Dfe.Testing.Pages.Public.Mapper;
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
