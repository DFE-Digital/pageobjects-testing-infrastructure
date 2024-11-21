namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDocumentQueryClientPublicAPI<TProvider>(this IServiceCollection services) where TProvider : class, IDocumentQueryClientProvider
        => services
            .AddScoped<IDocumentQueryClientProvider, TProvider>()
            .AddScoped<IDocumentQueryClientAccessor, DocumentQueryClientAccessor>()
            // Pages
            .AddScoped<IPageFactory, PageFactory>()
            // Common Components
            .AddTransient<AnchorLinkFactory>()
            .AddTransient<FormFactory>()
            .AddTransient<GDSFieldsetFactory>()
            .AddTransient<GDSCheckboxWithLabelFactory>()
            .AddTransient<GDSButtonFactory>()
            // Helpers    
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
