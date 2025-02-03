using Dfe.Testing.Pages.Public.Commands;
using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Shared.Selector;
using Shouldly;
using static Dfe.Testing.Pages.IntegrationTests.WebDriverIntegrationTestDesign;

namespace Dfe.Testing.Pages.IntegrationTests;

public sealed class WebDriverIntegrationTestDesign
{

    [Fact]
    public async Task WebDriver_Click_Handler_Works()
    {
        // Arrange
        using var collection = MockServiceCollection.WithWebDriver();

        IDocumentService documentService = collection.ServiceProvider.GetRequiredService<IDocumentService>();

        // Act
        await documentService.RequestDocumentAsync(
            (t) =>
                t.SetDomain("searchprototype.azurewebsites.net")
                    .SetPath("/")
                    .AddQueryParameter(new(key: "searchKeyWord", value: "Col")));

        collection.ServiceProvider
            .GetRequiredService<SearchPage>()
            .ClickAnchorLink();

        // Assert
    }

    [Fact]
    public async Task WebDriver_AnchorLink_Factory_Works()
    {
        using var collection = MockServiceCollection.WithWebDriver();

        IDocumentService documentService = collection.ServiceProvider.GetRequiredService<IDocumentService>();

        await documentService.RequestDocumentAsync(
            (t) =>
                t.SetDomain("searchprototype.azurewebsites.net")
                    .SetPath("/")
                    .AddQueryParameter(new(key: "searchKeyWord", value: "Col")));

        IEnumerable<AnchorLinkComponent> links = collection.ServiceProvider.GetRequiredService<SearchPage>().GetLinks();
        Assert.Equal(3, links.Count());
    }

    public sealed class SearchPage
    {
        private readonly ICommandHandler<ClickElementCommand> _clickElementHandler;
        private readonly IComponentFactory<AnchorLinkComponent> _anchorLink;

        public SearchPage(
            ICommandHandler<ClickElementCommand> clickElementHandler,
            IComponentFactory<AnchorLinkComponent> anchorLink)
        {
            _clickElementHandler = clickElementHandler;
            _anchorLink = anchorLink;
        }

        public void ClickAnchorLink() => _clickElementHandler.Handle(new()
        {
            Selector = new CssElementSelector("#home-link")
        });

        public IEnumerable<AnchorLinkComponent> GetLinks() => _anchorLink.CreateMany(new()
        {
            FindInScope = new CssElementSelector(".govuk-header")
        }).Select(t => t.Created);
    }
}

internal static class MockServiceCollection
{
    internal static IServiceScope WithWebDriver()
    {
        return new ServiceCollection()
            .AddWebDriver(t =>
            {
                t.Browser.CustomOptions.Add("--disable-dev-shm-usage");
                t.Browser.ShowBrowser = false;
            })
            .AddTransient<SearchPage>()
            .BuildServiceProvider()
            .CreateScope();
    }
}
