using Dfe.Testing.Pages.Public.PageObject;

namespace Dfe.Testing.Pages.IntegrationTests.Component.Button;
public sealed class GDSButtonComponentTests
{
    [Fact]
    public async Task Should_Query_DefaultGDSButton()
    {
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        IDocumentService docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/button"));

        GDSButtonPage buttonPage = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<GDSButtonPage>();

        GDSButtonComponent defaultButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Save and continue")
            .SetType("submit")
            .Build();
        buttonPage.GetNoScope().Should().Be(defaultButton);
    }

    [Fact]
    public async Task Should_Scoped_Query_DefaultGDSButton()
    {
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        IDocumentService docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/buttonnested"));

        GDSButtonPage buttonPage = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<GDSButtonPage>();

        GDSButtonComponent nestedDefaultButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Nested save and continue")
            .SetType("submit")
            .Build();

        buttonPage.GetWithScope().Should().Be(nestedDefaultButton);
    }

    [Fact]
    public async Task Should_Query_Many_DefaultGDSButton()
    {
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        IDocumentService docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/buttonnested"));

        GDSButtonPage buttonPage = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<GDSButtonPage>();

        GDSButtonComponent nestedButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Nested save and continue")
            .SetType("submit")
            .Build();

        GDSButtonComponent button = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Save and continue")
            .SetType("submit")
            .Build();

        buttonPage.GetManyNoScope().Should().BeEquivalentTo([button, nestedButton]);
    }

    [Fact]
    public async Task Should_Query_Many_WithScope_DefaultGDSButton()
    {
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        IDocumentService docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/buttonnested"));

        GDSButtonPage buttonPage = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<GDSButtonPage>();

        GDSButtonComponent nestedButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Nested save and continue")
            .SetType("submit")
            .Build();

        GDSButtonComponent button = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Save and continue")
            .SetType("submit")
            .Build();

        buttonPage.GetManyNoScope().Should().BeEquivalentTo([button, nestedButton]);
    }
}
