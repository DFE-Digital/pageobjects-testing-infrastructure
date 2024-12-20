﻿using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.Header;
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
            NavigationLinks = _linkFactory.GetMany(new QueryOptions()
            {
                InScope = new CssElementSelector(".govuk-header nav"),
            })
        };
    }
}
