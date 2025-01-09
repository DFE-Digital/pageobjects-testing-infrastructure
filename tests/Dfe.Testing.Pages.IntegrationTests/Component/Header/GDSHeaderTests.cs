using Dfe.Testing.Pages.IntegrationTests.Component.CookieBanner;
using Dfe.Testing.Pages.Public.Components.GDS.Header;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;
using Dfe.Testing.Pages.Public.PageObject;

namespace Dfe.Testing.Pages.IntegrationTests.Component.Header;
public sealed class GDSHeaderTests
{
    [Fact]
    public async Task Should_Query_DefaultServiceHeaderWithName()
    {

        using var scope = ComponentTestHelper.GetServices<GDSHeaderPage>();
        IDocumentService docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/header"));

        GDSHeaderPage headerPage = scope.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<GDSHeaderPage>();

        IAnchorLinkComponentBuilder linkBuilder = scope.ServiceProvider.GetRequiredService<IAnchorLinkComponentBuilder>();
        linkBuilder.SetLink("#")
            .SetText("GOV.UK")
            .SetOpensInNewTab(false);

        IAnchorLinkComponentBuilder navigationLinkBuilder = scope.ServiceProvider.GetRequiredService<IAnchorLinkComponentBuilder>();
        navigationLinkBuilder.SetLink("#")
            .SetText("Service name")
            .SetOpensInNewTab(false);

        GDSHeaderComponent expectedHeader = new()
        {
            GovUKLink = linkBuilder.Build(),
            NavigationLinks = [navigationLinkBuilder.Build()]
        };

        headerPage.GetHeaderWithNoScope().Should().Be(expectedHeader);
    }

    //TODO with scope
}
