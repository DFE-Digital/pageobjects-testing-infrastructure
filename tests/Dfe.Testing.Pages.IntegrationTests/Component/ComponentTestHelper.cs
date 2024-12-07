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
            .AddAngleSharp<Program>()
            .AddTransient<IPageObject, TPage>()
            .BuildServiceProvider()
            .CreateScope();

        IDocumentSessionClient session = scopedContainerWithPage.ServiceProvider.GetRequiredService<IDocumentSessionClient>();
        await session.RequestDocumentAsync(httpRequest);
        return session.GetPageObject<TPage>();
    }
}
