using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;
using FluentAssertions;

namespace Dfe.Testing.Pages.IntegrationTests.Component.CookieBanner;
public sealed class GDSCookieBannerComponentTests
{

    [Fact]
    public async Task Should_Query_DefaultGDSCookieBanner()
    {
        var page = await ComponentTestHelper.RequestPage<GDSCookieBannerPage>("/component/cookiebanner");

        GDSCookieBanner expectedDefaultCookieBanner = new()
        {
            Heading = "Cookies on [name of service]",
            CookieChoiceButtons =
            [
                new GDSButton()
                {
                    Text = "Accept additional cookies",
                    ButtonType = ButtonStyleType.Primary,
                    IsSubmit = true,
                    Value = "yes",
                    Name = "cookies[additional]"
                },
                new GDSButton()
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
