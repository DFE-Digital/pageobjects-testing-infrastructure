using Dfe.Testing.Pages.IntegrationTests.Component.Helper;
using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.PageObject;

namespace Dfe.Testing.Pages.IntegrationTests.Component;
public sealed class GDSCookieBannerComponentTests
{

    [Fact]
    public async Task Should_Query_DefaultGDSCookieBanner()
    {
        using var scope = ComponentTestHelper.GetServices<GDSCookieBannerPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/cookiebanner"));

        var cookieBannerPage = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<GDSCookieBannerPage>();
        var choiceAvailableBannerBuilder = scope.ServiceProvider.GetRequiredService<IGDSCookieChoiceAvailableBannerComponentBuilder>();

        var acceptCookiesButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Accept additional cookies")
                    .SetName("cookies[additional]")
                    .SetType("submit")
                    .SetValue("yes")
                    .Build();
        var rejectCookiesButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Reject additional cookies")
                    .SetName("cookies[additional]")
                    .SetType("submit")
                    .SetValue("no")
                    .Build();

        var anchorLink = scope.ServiceProvider.GetRequiredService<IAnchorLinkComponentBuilder>()
            .SetText("View cookies")
            .SetLink("#")
            .SetOpensInNewTab(false)
            .Build();

        choiceAvailableBannerBuilder.SetHeading("Cookies on [name of service]")
            .AddCookieChoiceButton(acceptCookiesButton)
            .AddCookieChoiceButton(rejectCookiesButton)
            .SetViewCookiesLink(anchorLink);

        var component = choiceAvailableBannerBuilder.Build();

        cookieBannerPage.GetBannerNoScope().Should().BeEquivalentTo(component);
    }
}


internal sealed class GDSCookieBannerPage : IPageObject
{
    private readonly ComponentFactory<GDSCookieChoiceAvailableBannerComponent> _cookieBannerFactory;

    public GDSCookieBannerPage(ComponentFactory<GDSCookieChoiceAvailableBannerComponent> cookieBannerFactory)
    {
        ArgumentNullException.ThrowIfNull(cookieBannerFactory);
        _cookieBannerFactory = cookieBannerFactory;
    }

    internal GDSCookieChoiceAvailableBannerComponent GetBannerNoScope() => _cookieBannerFactory.Create().Created;
}
