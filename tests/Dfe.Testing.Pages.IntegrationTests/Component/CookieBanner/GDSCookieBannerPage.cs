﻿using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.IntegrationTests.Component.CookieBanner;
internal sealed class GDSCookieBannerPage : IPage
{
    private readonly ComponentFactory<GDSCookieBannerComponent> _cookieBannerFactory;

    public GDSCookieBannerPage(ComponentFactory<GDSCookieBannerComponent> cookieBannerFactory)
    {
        ArgumentNullException.ThrowIfNull(cookieBannerFactory);
        _cookieBannerFactory = cookieBannerFactory;
    }

    internal GDSCookieBannerComponent GetBannerNoScope() => _cookieBannerFactory.Get();
}
