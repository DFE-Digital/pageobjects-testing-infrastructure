using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;

namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDocumentQueryClientPublicAPI<TProvider>(this IServiceCollection services) where TProvider : class, IDocumentQueryClientProvider
        => services
            .AddSingleton<IDocumentQueryClientProvider, TProvider>()
            .AddScoped<IDocumentQueryClientAccessor, DocumentQueryClientAccessor>()
            // Pages
            .AddSingleton<IPageFactory>((serviceProvider) =>
            {
                var scope = serviceProvider.CreateScope();
                var pageFactoryDelegates = new Dictionary<string, Func<PageBase>>()
                {
                    // nameof(TPage) = () => serviceProvider.GetRequiredService<TPage>()
                };
                return new PageFactory(pageFactoryDelegates);
            })
            .AddScoped<IPageProvider, PageProvider>()
            // Common Components
            .AddTransient<AnchorLinkFactory>()
            .AddTransient<FormFactory>()
            .AddTransient<GDSFieldsetFactory>()
            .AddTransient<GDSCheckboxWithLabelFactory>()
            .AddTransient<GDSButtonFactory>()
            // Helpers    
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
