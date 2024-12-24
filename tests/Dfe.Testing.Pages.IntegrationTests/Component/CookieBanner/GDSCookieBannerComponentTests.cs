using Dfe.Testing.Pages.Public.Components.Core.Link;
using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.IntegrationTests.Component.CookieBanner;
public sealed class GDSCookieBannerComponentTests
{

    [Fact]
    public async Task Should_Query_DefaultGDSCookieBanner()
    {
        var page = await ComponentTestHelper.RequestPage<GDSCookieBannerPage>("/component/cookiebanner");

        GDSCookieChoiceAvailableBannerComponent expectedDefaultCookieBanner = new()
        {
            Heading = new TextComponent() { Text = "Cookies on [name of service]" },
            CookieChoiceButtons =
            [
                new GDSButtonComponent()
                {
                    Text = new()
                    {
                        Text = "Accept additional cookies",
                    },
                    ButtonType = ButtonStyleType.Primary,
                    IsSubmit = true,
                    Value = "yes",
                    Name = "cookies[additional]"
                },
                new GDSButtonComponent()
                {
                    Text = new()
                    {
                        Text = "Reject additional cookies"
                    },
                    ButtonType = ButtonStyleType.Primary,
                    IsSubmit = true,
                    Value = "no",
                    Name = "cookies[additional]"
                }
            ],
            ViewCookiesLink = new AnchorLinkComponent()
            {
                LinkedTo = "#",
                Text = new()
                {
                    Text = "View cookies"
                },
                OpensInNewTab = false
            }
        };

        page.GetBannerNoScope().Should().BeEquivalentTo(expectedDefaultCookieBanner);
    }
}
