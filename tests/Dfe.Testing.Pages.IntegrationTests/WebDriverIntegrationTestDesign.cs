using Dfe.Testing.Pages.Components;
using static Dfe.Testing.Pages.IntegrationTests.WebDriverIntegrationTestDesign;

namespace Dfe.Testing.Pages.IntegrationTests;

public sealed class WebDriverIntegrationTestDesign
{

    [Fact]
    public async Task WebDriver_Click_Handler_Works()
    {
        using var collection = MockServiceCollection.WithWebDriver();

        IDocumentClientSession session = collection.ServiceProvider.GetRequiredService<IDocumentClientSession>();
        await session.RequestDocumentAsync(
            (t) =>
                t.SetDomain("searchprototype.azurewebsites.net")
                    .SetPath("/")
                    .AddQueryParameter(new(key: "searchKeyWord", value: "Col")));

        SearchPage searchPage = session.GetPage<SearchPage>();

        searchPage.ClickAnchorLink();
        IApplicationNavigatorAccessor navigatorAccessor = collection.ServiceProvider.GetRequiredService<IApplicationNavigatorAccessor>();
        navigatorAccessor.Navigator.CurrentUri().Should().Be(new Uri("https://searchprototype.azurewebsites.net/"));
    }

    [Fact]
    public async Task WebDriver_AnchorLink_Factory_Works()
    {
        using var collection = MockServiceCollection.WithWebDriver();

        IDocumentClientSession session = collection.ServiceProvider.GetRequiredService<IDocumentClientSession>();
        await session.RequestDocumentAsync(
            (t) =>
                t.SetDomain("searchprototype.azurewebsites.net")
                    .SetPath("/")
                    .AddQueryParameter(new(key: "searchKeyWord", value: "Col")));

        SearchPage page = session.GetPage<SearchPage>();
        page.GetLinks().Should().HaveCount(3);
    }

    public sealed class SearchPage : IPage
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
            FindWith = new CssElementSelector("#home-link")
        });

        public IEnumerable<AnchorLinkComponent> GetLinks() => _anchorLink.GetMany(new()
        {
            InScope = new CssElementSelector(".govuk-header")
        });
    }
}

public static class MockServiceCollection
{
    internal static IServiceScope WithWebDriver()
    {
        return new ServiceCollection()
            .AddWebDriver()
            .AddTransient<IPage, SearchPage>()
            .BuildServiceProvider()
            .CreateScope();
    }
}
