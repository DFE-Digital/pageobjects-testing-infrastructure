﻿using Dfe.Testing.Pages.IntegrationTests.Component.Helper;
using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Shared.Selector;
using Shouldly;

namespace Dfe.Testing.Pages.IntegrationTests.Component;
public sealed class GDSButtonComponentTests
{
    [Fact]
    public async Task Should_Query_DefaultGDSButton()
    {
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/button"));

        var buttonPage = scope.ServiceProvider.GetRequiredService<GDSButtonPage>();

        var defaultButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Save and continue")
            .SetType("submit")
            .Build();

        Assert.Equivalent(
            defaultButton,
            buttonPage.GetNoScope());
    }

    [Fact]
    public async Task Should_Scoped_Query_DefaultGDSButton()
    {
        // Arrange
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();

        // Act
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/buttonnested"));

        // Assert

        var nestedDefaultButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Nested save and continue")
            .SetType("submit")
            .Build();

        Assert.Equivalent(
            nestedDefaultButton,
            scope.ServiceProvider.GetRequiredService<GDSButtonPage>().GetWithScope());
    }

    [Fact]
    public async Task Should_Query_Many_DefaultGDSButton()
    {
        // Arrange
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();

        // Act
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/buttonnested"));

        // Assert
        var nestedButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Nested save and continue")
            .SetType("submit")
            .Build();

        var button = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Save and continue")
            .SetType("submit")
            .Build();

        IEnumerable<GDSButtonComponent> expectedButtons = [button, nestedButton];

        Assert.Equivalent(
            expectedButtons,
            scope.ServiceProvider.GetRequiredService<GDSButtonPage>().GetManyNoScope());
    }

    [Fact]
    public async Task Should_Query_Many_WithScope_DefaultGDSButton()
    {
        // Arrange
        using var scope = ComponentTestHelper.GetServices<GDSButtonPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();

        // Act
        await docService.RequestDocumentAsync(t => t.SetPath("/component/button/buttonnested"));

        // Assert
        var nestedButton = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Nested save and continue")
            .SetType("submit")
            .Build();

        var button = scope.ServiceProvider.GetRequiredService<IGDSButtonBuilder>()
            .SetText("Save and continue")
            .SetType("submit")
            .Build();

        IEnumerable<GDSButtonComponent> expectedButtons = [button, nestedButton];

        Assert.Equivalent(
            expectedButtons,
            scope.ServiceProvider.GetRequiredService<GDSButtonPage>().GetManyWithScope());
    }
}


internal sealed class GDSButtonPage
{
    private readonly IComponentFactory<GDSButtonComponent> _buttonFactory;

    public GDSButtonPage(IComponentFactory<GDSButtonComponent> buttonFactory)
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
