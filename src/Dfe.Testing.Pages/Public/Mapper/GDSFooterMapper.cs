using Dfe.Testing.Pages.Components.Footer;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class GDSFooterMapper : IComponentMapper<GDSFooterComponent>
{
    private readonly ComponentFactory<AnchorLinkComponent> _anchorLinKFactory;
    private readonly IComponentDefaultSelectorFactory _componentSelectorFactory;

    public GDSFooterMapper(ComponentFactory<AnchorLinkComponent> anchorLinKFactory, IComponentDefaultSelectorFactory componentSelectorFactory)
    {
        ArgumentNullException.ThrowIfNull(anchorLinKFactory);
        _componentSelectorFactory = componentSelectorFactory;
        _anchorLinKFactory = anchorLinKFactory;
    }
    public GDSFooterComponent Map(IDocumentPart input)
    {
        return new()
        {
            CrownCopyrightLink = _anchorLinKFactory.Get(new QueryRequestArgs()
            {
                Query = new CssSelector(".govuk-footer__copyright-logo"),
                Scope = _componentSelectorFactory.GetSelector<GDSFooterComponent>()
            }),
            LicenseLink = _anchorLinKFactory.Get(new QueryRequestArgs()
            {
                Query = new CssSelector(".govuk-footer__copyright-logo"),
                Scope = _componentSelectorFactory.GetSelector<GDSFooterComponent>()
            }),
            LicenseMessage = input.GetChild(new CssSelector(".govuk-footer__licence-description"))?.Text ?? string.Empty,
            ApplicationLinks = _anchorLinKFactory.GetMany(new QueryRequestArgs()
            {
                Query = new CssSelector(".govuk-footer__inline-list"),
                Scope = _componentSelectorFactory.GetSelector<GDSFooterComponent>()
            }) ?? []
        };
    }
}
