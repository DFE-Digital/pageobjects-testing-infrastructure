using Dfe.Testing.Pages.IntegrationTests.Component.Helper;
using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Public.PageObject;
using Dfe.Testing.Pages.Shared.Selector;

namespace Dfe.Testing.Pages.IntegrationTests.Component;
public sealed class GDSButtonComponentTests
{
    [Fact]
    public async Task Should_Query_DefaultGDSButton()
    {
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/button"));

        var buttonPage = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<GDSButtonPage>();

        var defaultButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Save and continue")
            .SetType("submit")
            .Build();
        buttonPage.GetNoScope().Should().Be(defaultButton);
    }

    [Fact]
    public async Task Should_Scoped_Query_DefaultGDSButton()
    {
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/buttonnested"));

        var buttonPage = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<GDSButtonPage>();

        var nestedDefaultButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Nested save and continue")
            .SetType("submit")
            .Build();

        buttonPage.GetWithScope().Should().Be(nestedDefaultButton);
    }

    [Fact]
    public async Task Should_Query_Many_DefaultGDSButton()
    {
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/buttonnested"));

        var buttonPage = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<GDSButtonPage>();

        var nestedButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Nested save and continue")
            .SetType("submit")
            .Build();

        var button = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Save and continue")
            .SetType("submit")
            .Build();

        buttonPage.GetManyNoScope().Should().BeEquivalentTo([button, nestedButton]);
    }

    [Fact]
    public async Task Should_Query_Many_WithScope_DefaultGDSButton()
    {
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/buttonnested"));

        var buttonPage = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<GDSButtonPage>();

        var nestedButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Nested save and continue")
            .SetType("submit")
            .Build();

        var button = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Save and continue")
            .SetType("submit")
            .Build();

        buttonPage.GetManyNoScope().Should().BeEquivalentTo([button, nestedButton]);
    }
}


internal sealed class GDSButtonPage : IPageObject
{
    private readonly ComponentFactory<GDSButtonComponent> _buttonFactory;

    public GDSButtonPage(ComponentFactory<GDSButtonComponent> buttonFactory)
    {
        _buttonFactory = buttonFactory;
    }

    public GDSButtonComponent GetNoScope() => _buttonFactory.Create().Created;
    public GDSButtonComponent GetWithScope() => _buttonFactory.Create(new CreateComponentRequest()
    {
        FindInScope = new CssElementSelector("#gds-button-default-nested-nested-container")
    }).Created;
    public IEnumerable<GDSButtonComponent> GetManyNoScope() => _buttonFactory.CreateMany().Select(t => t.Created);
    public IEnumerable<GDSButtonComponent> GetManyWithScope() => _buttonFactory.CreateMany(new CreateComponentRequest()
    {
        FindInScope = new CssElementSelector("#gds-button-default-nested-container")
    }).Select(t => t.Created);
}
