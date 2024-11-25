using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.IntegrationTests.Component.CookieBanner;
internal sealed class GDSCookieBannerPage : IPage
{
    private readonly ComponentFactory<GDSCookieBanner> _cookieBannerFactory;

    public GDSCookieBannerPage(ComponentFactory<GDSCookieBanner> cookieBannerFactory)
    {
        ArgumentNullException.ThrowIfNull(cookieBannerFactory);
        _cookieBannerFactory = cookieBannerFactory;
    }

    internal GDSCookieBanner GetBannerNoScope() => _cookieBannerFactory.Get();
}
