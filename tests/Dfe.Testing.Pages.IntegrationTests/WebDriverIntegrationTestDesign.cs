using Dfe.Testing.Pages.Public.Commands;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Selector;
using Dfe.Testing.Pages.Public.WebDriver;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly ComponentFactory<AnchorLink> _anchorLink;

        public SearchPage(
            ICommandHandler<ClickElementCommand> clickElementHandler,
            ComponentFactory<AnchorLink> anchorLink)
        {
            _clickElementHandler = clickElementHandler;
            _anchorLink = anchorLink;
        }

        public void ClickAnchorLink() => _clickElementHandler.Handle(new()
        {
            FindWith = new CssSelector("#home-link")
        });

        public IEnumerable<AnchorLink> GetLinks() => _anchorLink.GetMany(new()
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
