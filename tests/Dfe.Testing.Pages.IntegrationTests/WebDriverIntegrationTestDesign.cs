﻿using Dfe.Testing.Pages.Internal.WebDriver.Provider.Adaptor;
using static Dfe.Testing.Pages.IntegrationTests.WebDriverIntegrationTestDesign;

namespace Dfe.Testing.Pages.IntegrationTests;

public sealed class WebDriverIntegrationTestDesign
{

    [Fact]
    public async Task WebDriver_Click_Handler_Works()
    {
        using var collection = MockServiceCollection.WithWebDriver();

        IDocumentSessionClient session = collection.ServiceProvider.GetRequiredService<IDocumentSessionClient>();
        await session.RequestDocumentAsync(
            (t) =>
                t.SetDomain("searchprototype.azurewebsites.net")
                    .SetPath("/")
                    .AddQueryParameter(new(key: "searchKeyWord", value: "Col")));

        SearchPage searchPage = session.GetPageObject<SearchPage>();

        searchPage.ClickAnchorLink();
        var driverAdaptorProvider = collection.ServiceProvider.GetRequiredService<IWebDriverAdaptorProvider>();
        IWebDriverAdaptor driverAdaptor = await driverAdaptorProvider.GetAsync();
        await driverAdaptor.NavigateToAsync(new Uri("https://searchprototype.azurewebsites.net/"));
        driverAdaptor.CurrentUri().Should().Be("https://searchprototype.azurewebsites.net/");
    }

    [Fact]
    public async Task WebDriver_AnchorLink_Factory_Works()
    {
        using var collection = MockServiceCollection.WithWebDriver();

        IDocumentSessionClient session = collection.ServiceProvider.GetRequiredService<IDocumentSessionClient>();
        await session.RequestDocumentAsync(
            (t) =>
                t.SetDomain("searchprototype.azurewebsites.net")
                    .SetPath("/")
                    .AddQueryParameter(new(key: "searchKeyWord", value: "Col")));

        SearchPage page = session.GetPageObject<SearchPage>();
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
            .AddTransient<IPageObject, SearchPage>()
            .BuildServiceProvider()
            .CreateScope();
    }
}
