using Dfe.Testing.Pages.Internal.ComponentFactory;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Components;
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
                    // client registers nameof(TPage) = () => serviceProvider.GetRequiredService<TPage>()
                };
                return new PageFactory(pageFactoryDelegates);
            })
            .AddScoped<IPageProvider, PageProvider>()
            // Common Components factories clients call for
            .AddTransient<ComponentFactory<AnchorLink>, AnchorLinkComponentFactory>()
            .AddTransient<ComponentFactory<Form>, FormComponentFactory>()
            .AddTransient<ComponentFactory<GDSFieldset>, GDSFieldsetComponentFactory>()
            .AddTransient<ComponentFactory<GDSCheckboxWithLabel>, GDSCheckboxWithLabelComponentFactory>()
            .AddTransient<ComponentFactory<GDSButton>, GDSButtonComponentFactory>()
            // Helpers    
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
