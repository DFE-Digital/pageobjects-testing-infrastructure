﻿using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Selector.Factory;

namespace Dfe.Testing.Pages.Public.Components.GDS.Footer;
internal sealed class GDSFooterMapper : IComponentMapper<GDSFooterComponent>
{
    private readonly ComponentFactory<AnchorLinkComponent> _anchorLinKFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;

    public GDSFooterMapper(ComponentFactory<AnchorLinkComponent> anchorLinKFactory, IComponentSelectorFactory componentSelectorFactory)
    {
        ArgumentNullException.ThrowIfNull(anchorLinKFactory);
        _componentSelectorFactory = componentSelectorFactory;
        _anchorLinKFactory = anchorLinKFactory;
    }
    public GDSFooterComponent Map(IDocumentPart input)
    {
        return new()
        {
            CrownCopyrightLink = _anchorLinKFactory.Get(new QueryOptions()
            {
                Query = new CssElementSelector(".govuk-footer__copyright-logo"),
                InScope = _componentSelectorFactory.GetSelector<GDSFooterComponent>()
            }),
            LicenseLink = _anchorLinKFactory.Get(new QueryOptions()
            {
                Query = new CssElementSelector(".govuk-footer__copyright-logo"),
                InScope = _componentSelectorFactory.GetSelector<GDSFooterComponent>()
            }),
            LicenseMessage = input.FindDescendant(new CssElementSelector(".govuk-footer__licence-description"))?.Text ?? string.Empty,
            ApplicationLinks = _anchorLinKFactory.GetMany(new QueryOptions()
            {
                Query = new CssElementSelector(".govuk-footer__inline-list"),
                InScope = _componentSelectorFactory.GetSelector<GDSFooterComponent>()
            }) ?? []
        };
    }
}
