namespace Dfe.Testing.Pages.IntegrationTests.Component.Button;
public sealed class GDSButtonComponentTests
{
    [Fact]
    public async Task Should_Query_DefaultGDSButton()
    {
        var page = await ComponentTestHelper.RequestPage<GDSButtonPage>("/component/button/button");

        GDSButtonComponent expectedDefaultButton = new()
        {
            ButtonType = ButtonStyleType.Primary,
            Text = "Save and continue",
            IsSubmit = true,
            Disabled = false
        };

        page.GetNoScope().Should().Be(expectedDefaultButton);
    }

    [Fact]
    public async Task Should_Scoped_Query_DefaultGDSButton()
    {
        var page = await ComponentTestHelper.RequestPage<GDSButtonPage>("/component/button/buttonnested");

        GDSButtonComponent expectedDefaultButton = new()
        {
            ButtonType = ButtonStyleType.Primary,
            Text = "Nested save and continue",
            IsSubmit = true,
            Disabled = false
        };

        page.GetWithScope().Should().Be(expectedDefaultButton);
    }

    [Fact]
    public async Task Should_Query_Many_DefaultGDSButton()
    {
        var page = await ComponentTestHelper.RequestPage<GDSButtonPage>("/component/button/buttonnested");

        page.GetManyNoScope().Should().BeEquivalentTo(
            new GDSButtonComponent[]
            {
                new()
                {
                    ButtonType = ButtonStyleType.Primary,
                    Text = "Save and continue",
                    IsSubmit = true,
                    Disabled = false
                },
                new()
                {
                    ButtonType = ButtonStyleType.Primary,
                    Text = "Nested save and continue",
                    IsSubmit = true,
                    Disabled = false
                }
            });
    }

    [Fact]
    public async Task Should_Query_Many_WithScope_DefaultGDSButton()
    {
        var page = await ComponentTestHelper.RequestPage<GDSButtonPage>("/component/button/buttonnested");

        page.GetManyWithScope().Should().BeEquivalentTo(
            new GDSButtonComponent[]
            {
                new()
                {
                    ButtonType = ButtonStyleType.Primary,
                    Text = "Save and continue",
                    IsSubmit = true,
                    Disabled = false
                },
                new()
                {
                    ButtonType = ButtonStyleType.Primary,
                    Text = "Nested save and continue",
                    IsSubmit = true,
                    Disabled = false
                }
            });
    }
}
