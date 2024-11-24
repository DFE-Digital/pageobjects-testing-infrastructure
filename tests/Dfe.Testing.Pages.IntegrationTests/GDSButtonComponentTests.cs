using Dfe.Testing.Pages.IntegrationTests.Pages;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Dfe.Testing.Pages.IntegrationTests;
public sealed class GDSButtonComponentTests
{
    [Fact]
    public async Task Should_Query_DefaultGDSButton()
    {
        var page = await GetButtonPage("/component/button");

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
        var page = await GetButtonPage("/component/buttonnested");

        GDSButton expectedDefaultButton = new()
        {
            ButtonType = ButtonStyleType.Primary,
            Text = "Nested save and continue",
            IsSubmit = true,
            Disabled = false
        };

        page.GetDefaultGDSButtonWithScope().Should().Be(expectedDefaultButton);
    }

    private static async Task<GDSButtonPage> GetButtonPage(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        using HttpRequestMessage httpRequest = new()
        {
            RequestUri = new(path, UriKind.Relative)
        };

        var scope = WithAngleSharpProvider();
        GDSButtonPage page = await scope.ServiceProvider.GetRequiredService<IPageFactory>()
            .CreatePageAsync<GDSButtonPage>(httpRequest);
        return page;
    }

    private static IServiceScope WithAngleSharpProvider()
    {
        return new ServiceCollection()
            .AddAngleSharp<Program>()
            .AddTransient<IPage, GDSButtonPage>()
            .BuildServiceProvider()
            .CreateScope();
    }
}
