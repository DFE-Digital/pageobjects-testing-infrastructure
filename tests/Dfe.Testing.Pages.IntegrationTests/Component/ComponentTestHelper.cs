using System.ComponentModel.Design;
using Dfe.Testing.Pages.Public.PageObject;

namespace Dfe.Testing.Pages.IntegrationTests.Component;
internal static class ComponentTestHelper
{
    internal static async Task<TPage> RequestPage<TPage>(string path) where TPage : class, IPageObject
    {
        ArgumentNullException.ThrowIfNull(path);
        using HttpRequestMessage httpRequest = new()
        {
            RequestUri = new(path, UriKind.Relative)
        };

        var scopedContainerWithPage = new ServiceCollection()
            .AddAngleSharp()
            .AddWebApplicationFactory<Program>()
            .AddTransient<IPageObject, TPage>()
            .BuildServiceProvider()
            .CreateScope();

        IDocumentService session = scopedContainerWithPage.ServiceProvider.GetRequiredService<IDocumentService>();
        await session.RequestDocumentAsync(httpRequest);
        return scopedContainerWithPage.ServiceProvider.GetRequiredService<IPageObjectFactory>().Create<TPage>();
    }

    internal static IServiceScope GetServices<TPage>(Action<IServiceCollection>? configure = null) where TPage : class, IPageObject
    {
        var scopedContainerWithPage = new ServiceCollection()
            .AddAngleSharp()
            .AddWebApplicationFactory<Program>()
            .AddTransient<IPageObject, TPage>();

        configure?.Invoke(scopedContainerWithPage);

        return scopedContainerWithPage.BuildServiceProvider()
            .CreateScope();
    }
}
