﻿using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Components.CookieBanner;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSCookieBannerMapper : IComponentMapper<GDSCookieBannerComponent>
{
    private static readonly CssElementSelector Container = new(".govuk-cookie-banner");
    private readonly ComponentFactory<GDSButtonComponent> _buttonFactory;
    private readonly ComponentFactory<AnchorLinkComponent> _linkFactory;

    public GDSCookieBannerMapper(
        ComponentFactory<GDSButtonComponent> buttonFactory,
        ComponentFactory<AnchorLinkComponent> linkFactory)
    {
        ArgumentNullException.ThrowIfNull(buttonFactory);
        ArgumentNullException.ThrowIfNull(linkFactory);
        _buttonFactory = buttonFactory;
        _linkFactory = linkFactory;
    }
    public GDSCookieBannerComponent Map(IDocumentPart input)
    {
        return new()
        {
            Heading = input.GetChild(new CssElementSelector(".govuk-cookie-banner__heading"))!.Text.Trim(),
            //Content = documentPart.GetChild(new CssSelector(".govuk-cookie-banner__content"))!.Text,
            CookieChoiceButtons = _buttonFactory.GetMany(new QueryOptions()
            {
                InScope = Container
            }),
            ViewCookiesLink = _linkFactory.Get(new QueryOptions()
            {
                InScope = Container
            }),
            TagName = input.TagName
        };
    }
}
