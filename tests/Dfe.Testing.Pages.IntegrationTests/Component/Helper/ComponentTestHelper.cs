using Dfe.Testing.Pages.Contracts.Documents;

namespace Dfe.Testing.Pages.IntegrationTests.Component.Helper;
internal static class ComponentTestHelper
{
    internal static async Task<TPage> RequestPage<TPage>(string path) where TPage : class
    {
        ArgumentNullException.ThrowIfNull(path);
        using HttpRequestMessage httpRequest = new()
        {
            RequestUri = new(path, UriKind.Relative)
        };

        var scopedContainerWithPage = new ServiceCollection()
            .AddAngleSharp()
            .AddWebApplicationFactory<Program>()
            .AddTransient<TPage>()
            .BuildServiceProvider()
            .CreateScope();

        var session = scopedContainerWithPage.ServiceProvider.GetRequiredService<IDocumentService>();
        await session.RequestDocumentAsync(httpRequest);
        return scopedContainerWithPage.ServiceProvider.GetRequiredService<TPage>();
    }

    internal static IServiceScope GetServices<TPage>(Action<IServiceCollection>? configure = null) where TPage : class
    {
        var scopedContainerWithPage = new ServiceCollection()
            .AddAngleSharp()
            .AddWebApplicationFactory<Program>()
            .AddTransient<TPage>();

        configure?.Invoke(scopedContainerWithPage);

        return scopedContainerWithPage.BuildServiceProvider()
            .CreateScope();
    }
}
