using Dfe.Testing.Pages.Public.Components.Core.Text;
using Dfe.Testing.Pages.Public.Components.GDS.Header;

namespace Dfe.Testing.Pages.IntegrationTests.Component.Header;
public sealed class GDSHeaderTests
{
    [Fact]
    public async Task Should_Query_DefaultServiceHeaderWithName()
    {
        var page = await ComponentTestHelper.RequestPage<GDSHeaderPage>("/component/header");

        GDSHeaderComponent expectedHeader = new()
        {
            GovUKLink = new()
            {
                LinkedTo = "#",
                Text = new TextComponent()
                {
                    Text = "GOV.UK"
                },
                OpensInNewTab = false
            },
            NavigationLinks =
            [
                new()
                {
                    LinkedTo = "#",
                    Text = new TextComponent()
                    {
                        Text = "Service name"
                    },
                    OpensInNewTab = false
                }
            ]
        };

        page.GetHeaderWithNoScope().Should().Be(expectedHeader);
    }

    //TODO with scope
}
