namespace Dfe.Testing.Pages.IntegrationTests.Component.CookieBanner;
internal sealed class GDSCookieBannerPage : IPageObject
{
    private readonly ComponentFactory<GDSCookieChoiceAvailableBannerComponent> _cookieBannerFactory;

    public GDSCookieBannerPage(ComponentFactory<GDSCookieChoiceAvailableBannerComponent> cookieBannerFactory)
    {
        ArgumentNullException.ThrowIfNull(cookieBannerFactory);
        _cookieBannerFactory = cookieBannerFactory;
    }

    internal GDSCookieChoiceAvailableBannerComponent GetBannerNoScope() => _cookieBannerFactory.Get();
}
