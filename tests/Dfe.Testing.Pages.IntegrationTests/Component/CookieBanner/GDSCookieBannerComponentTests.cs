using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.PageObject;

namespace Dfe.Testing.Pages.IntegrationTests.Component.CookieBanner;
public sealed class GDSCookieBannerComponentTests
{

    [Fact]
    public async Task Should_Query_DefaultGDSCookieBanner()
    {
        using var scope = ComponentTestHelper.GetServices<GDSCookieBannerPage>();
        IDocumentService docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/cookiebanner"));

        GDSCookieBannerPage cookieBannerPage = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<GDSCookieBannerPage>();
        IGDSCookieChoiceAvailableBannerComponentBuilder choiceAvailableBannerBuilder = scope.ServiceProvider.GetRequiredService<IGDSCookieChoiceAvailableBannerComponentBuilder>();

        GDSButtonComponent acceptCookiesButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Accept additional cookies")
                    .SetName("cookies[additional]")
                    .SetType("submit")
                    .SetValue("yes")
                    .Build();
        GDSButtonComponent rejectCookiesButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Reject additional cookies")
                    .SetName("cookies[additional]")
                    .SetType("submit")
                    .SetValue("no")
                    .Build();

        AnchorLinkComponent anchorLink = scope.ServiceProvider.GetRequiredService<IAnchorLinkComponentBuilder>()
            .SetText("View cookies")
            .SetLink("#")
            .SetOpensInNewTab(false)
            .Build();

        choiceAvailableBannerBuilder.SetHeading("Cookies on [name of service]")
            .AddCookieChoiceButton(acceptCookiesButton)
            .AddCookieChoiceButton(rejectCookiesButton)
            .SetViewCookiesLink(anchorLink);

        GDSCookieChoiceAvailableBannerComponent component = choiceAvailableBannerBuilder.Build();

        cookieBannerPage.GetBannerNoScope().Should().BeEquivalentTo(component);
    }
}
