using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;
using FluentAssertions;

namespace Dfe.Testing.Pages.IntegrationTests.Component.Button;
public sealed class GDSButtonComponentTests
{
    [Fact]
    public async Task Should_Query_DefaultGDSButton()
    {
        var page = await ComponentTestHelper.RequestPage<GDSButtonPage>("/component/button");

        GDSButton expectedDefaultButton = new()
        {
            ButtonType = ButtonStyleType.Primary,
            Text = "Save and continue",
            IsSubmit = true,
            Disabled = false
        };

        page.GetDefaultGDSButton().Should().Be(expectedDefaultButton);
    }

    [Fact]
    public async Task Should_Scoped_Query_DefaultGDSButton()
    {
        var page = await ComponentTestHelper.RequestPage<GDSButtonPage>("/component/buttonnested");

        GDSButton expectedDefaultButton = new()
        {
            ButtonType = ButtonStyleType.Primary,
            Text = "Nested save and continue",
            IsSubmit = true,
            Disabled = false
        };

        page.GetDefaultGDSButtonWithScope().Should().Be(expectedDefaultButton);
    }
}
