using Dfe.Testing.Pages.Components.AnchorLink;
using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Components.CookieBanner;
using FluentAssertions;

namespace Dfe.Testing.Pages.IntegrationTests.Component.CookieBanner;
public sealed class GDSCookieBannerComponentTests
{

    [Fact]
    public async Task Should_Query_DefaultGDSCookieBanner()
    {
        var page = await ComponentTestHelper.RequestPage<GDSCookieBannerPage>("/component/cookiebanner");

        GDSCookieBannerComponent expectedDefaultCookieBanner = new()
        {
            Heading = "Cookies on [name of service]",
            CookieChoiceButtons =
            [
                new GDSButtonComponent()
                {
                    Text = "Accept additional cookies",
                    ButtonType = ButtonStyleType.Primary,
                    IsSubmit = true,
                    Value = "yes",
                    Name = "cookies[additional]"
                },
                new GDSButtonComponent()
                {
                    Text = "Reject additional cookies",
                    ButtonType = ButtonStyleType.Primary,
                    IsSubmit = true,
                    Value = "no",
                    Name = "cookies[additional]"
                }
            ],
            ViewCookiesLink = new AnchorLinkComponent()
            {
                LinkValue = "#",
                Text = "View cookies",
                OpensInNewTab = false
            }
        };

        page.GetBannerNoScope().Should().BeEquivalentTo(expectedDefaultCookieBanner);
    }
}
