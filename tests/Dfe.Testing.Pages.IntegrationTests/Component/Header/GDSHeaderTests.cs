using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;
using FluentAssertions;

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
                LinkValue = "#",
                Text = "GOV.UK",
                OpensInNewTab = false
            },
            ServiceName = new()
            {
                LinkValue = "#",
                Text = "Service name",
                OpensInNewTab = false
            },
            TagName = "header"
        };

        page.GetHeaderWithNoScope().Should().Be(expectedHeader);
    }

    //TODO with scope
}
