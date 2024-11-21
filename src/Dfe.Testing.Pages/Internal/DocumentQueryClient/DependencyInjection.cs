using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;

namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDocumentQueryClientPublicAPI<TProvider>(this IServiceCollection services) where TProvider : class, IDocumentQueryClientProvider
        => services
            .AddScoped<IDocumentQueryClientProvider, TProvider>()
            .AddScoped<IDocumentQueryClientAccessor, DocumentQueryClientAccessor>()
            // Pages
            .AddScoped<IPageProvider, PageProvider>()
            .AddSingleton<IPageFactory>((serviceProvider) =>
            {
                var scope = serviceProvider.CreateScope();
                var pageFactoryDelegates = new Dictionary<string, Func<PageBase>>()
                {
                    // nameof(TPage) = () => serviceProvider.GetRequiredService<TPage>()
                };
                return new PageFactory(pageFactoryDelegates);
            })
            // Common Components
            .AddTransient<AnchorLinkFactory>()
            .AddTransient<FormFactory>()
            .AddTransient<GDSFieldsetFactory>()
            .AddTransient<GDSCheckboxWithLabelFactory>()
            .AddTransient<GDSButtonFactory>()
            // Helpers    
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
