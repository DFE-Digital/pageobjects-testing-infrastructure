using Dfe.Testing.Pages.IntegrationTests.Component.Helper;
using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Public.Components.GDS.Header;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.PageObject;

namespace Dfe.Testing.Pages.IntegrationTests.Component;
public sealed class GDSHeaderTests
{
    [Fact]
    public async Task Should_Query_DefaultServiceHeaderWithName()
    {
        // Arrange
        using var scope = ComponentTestHelper.GetServices<GDSHeaderPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        var pageObjectFactory = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>();

        // Act
        await docService.RequestDocumentAsync((t) => t.SetPath("/component/header/header"));

        var headerPage = pageObjectFactory.Create<GDSHeaderPage>();

        var govUkLink = scope.ServiceProvider.GetRequiredService<IAnchorLinkComponentBuilder>()
            .SetLink("#")
            .SetText("GOV.UK")
            .SetOpensInNewTab(false)
            .Build();

        var navigationLink = scope.ServiceProvider.GetRequiredService<IAnchorLinkComponentBuilder>()
            .SetLink("#")
            .SetText("Service name")
            .SetOpensInNewTab(false)
            .Build();

        GDSHeaderComponent expectedHeader = new()
        {
            GovUKLink = govUkLink,
            NavigationLinks = [navigationLink]
        };

        headerPage.GetHeaderWithNoScope().Should().BeEquivalentTo(expectedHeader);
    }

    //TODO with scope
}


internal sealed class GDSHeaderPage : IPageObject
{
    private readonly ComponentFactory<GDSHeaderComponent> _headerFactory;

    public GDSHeaderPage(ComponentFactory<GDSHeaderComponent> headerFactory)
    {
        _headerFactory = headerFactory;
    }

    public GDSHeaderComponent GetHeaderWithNoScope() => _headerFactory.Create().Created;
}

