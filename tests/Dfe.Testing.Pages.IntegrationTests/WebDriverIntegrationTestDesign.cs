using Dfe.Testing.Pages.Internal.WebDriver.Provider.Adaptor;
using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.PageObject;
using Dfe.Testing.Pages.Shared.Contracts;
using Dfe.Testing.Pages.Shared.Selector;
using static Dfe.Testing.Pages.IntegrationTests.WebDriverIntegrationTestDesign;

namespace Dfe.Testing.Pages.IntegrationTests;

public sealed class WebDriverIntegrationTestDesign
{

    [Fact]
    public async Task WebDriver_Click_Handler_Works()
    {
        using var collection = MockServiceCollection.WithWebDriver();

        IDocumentService documentService = collection.ServiceProvider.GetRequiredService<IDocumentService>();
        IPageObjectFactory pageObjectFactory = collection.ServiceProvider.GetRequiredService<IPageObjectFactory>();

        await documentService.RequestDocumentAsync(
            (t) =>
                t.SetDomain("searchprototype.azurewebsites.net")
                    .SetPath("/")
                    .AddQueryParameter(new(key: "searchKeyWord", value: "Col")));

        SearchPage searchPage = pageObjectFactory.Create<SearchPage>();

        searchPage.ClickAnchorLink();
    }

    [Fact]
    public async Task WebDriver_AnchorLink_Factory_Works()
    {
        using var collection = MockServiceCollection.WithWebDriver();

        IDocumentService documentService = collection.ServiceProvider.GetRequiredService<IDocumentService>();
        IPageObjectFactory pageObjectFactory = collection.ServiceProvider.GetRequiredService<IPageObjectFactory>();

        await documentService.RequestDocumentAsync(
            (t) =>
                t.SetDomain("searchprototype.azurewebsites.net")
                    .SetPath("/")
                    .AddQueryParameter(new(key: "searchKeyWord", value: "Col")));

        SearchPage page = pageObjectFactory.Create<SearchPage>();
        page.GetLinks().Should().HaveCount(3);
    }

    public sealed class SearchPage : IPageObject
    {
        private readonly ICommandHandler<ClickElementCommand> _clickElementHandler;
        private readonly ComponentFactory<AnchorLinkComponent> _anchorLink;

        public SearchPage(
            ICommandHandler<ClickElementCommand> clickElementHandler,
            ComponentFactory<AnchorLinkComponent> anchorLink)
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

public static class MockServiceCollection
{
    internal static IServiceScope WithWebDriver()
    {
        return new ServiceCollection()
            .AddWebDriver(t =>
            {
                t.Browser.CustomOptions.Add("--disable-dev-shm-usage");
                t.Browser.ShowBrowser = false;
            })
            .AddTransient<IPageObject, SearchPage>()
            .BuildServiceProvider()
            .CreateScope();
    }
}
