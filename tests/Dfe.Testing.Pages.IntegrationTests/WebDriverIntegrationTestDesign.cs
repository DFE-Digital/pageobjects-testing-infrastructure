﻿
using static Dfe.Testing.Pages.IntegrationTests.WebDriverIntegrationTestDesign;

namespace Dfe.Testing.Pages.IntegrationTests;

public sealed class WebDriverIntegrationTestDesign
{

    [Fact]
    public async Task WebDriver_Click_Handler_Works()
    {
        using var collection = MockServiceCollection.WithWebDriver();
        HttpRequestMessage request = new()
        {
            RequestUri = new("https://searchprototype.azurewebsites.net/?searchKeyWord=Col")
        };

        SearchPage page = await collection.ServiceProvider.GetRequiredService<IPageFactory>().CreatePageAsync<SearchPage>(request);
        page.ClickAnchorLink();
        IApplicationNavigatorAccessor navigatorAccessor = collection.ServiceProvider.GetRequiredService<IApplicationNavigatorAccessor>();
        navigatorAccessor.Navigator.CurrentUri().Should().Be(new Uri("https://searchprototype.azurewebsites.net/"));
    }

    [Fact]
    public async Task WebDriver_AnchorLink_Factory_Works()
    {
        using var collection = MockServiceCollection.WithWebDriver();
        HttpRequestMessage request = new()
        {
            RequestUri = new("https://searchprototype.azurewebsites.net/?searchKeyWord=Col")
        };

        SearchPage page = await collection.ServiceProvider.GetRequiredService<IPageFactory>().CreatePageAsync<SearchPage>(request);
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
            FindWith = new CssSelector("#home-link")
        });

        public IEnumerable<AnchorLinkComponent> GetLinks() => _anchorLink.GetMany(new()
        {
            Scope = new CssSelector(".govuk-header")
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
