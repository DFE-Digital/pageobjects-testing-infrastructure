using Dfe.Testing.Pages.Internal.ComponentFactory;
using Dfe.Testing.Pages.Internal.DocumentQueryClient.Commands;
using Dfe.Testing.Pages.Public.Commands;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDocumentQueryClient<TProvider>(this IServiceCollection services) where TProvider : class, IDocumentQueryClientProvider
        => services
            .AddScoped<IDocumentQueryClientProvider, TProvider>()
            .AddScoped<IDocumentQueryClientAccessor, DocumentQueryClientAccessor>()
            // Pages
            .AddScoped<IPageFactory, PageFactory>()
            // Common component factories clients
            .AddTransient<ComponentFactory<AnchorLink>, AnchorLinkComponentFactory>()
            .AddTransient<ComponentFactory<Form>, FormComponentFactory>()
            .AddTransient<ComponentFactory<GDSHeader>, GDSHeaderComponentFactory>()
            .AddTransient<ComponentFactory<GDSFieldset>, GDSFieldsetComponentFactory>()
            .AddTransient<ComponentFactory<GDSCheckboxWithLabel>, GDSCheckboxWithLabelComponentFactory>()
            .AddTransient<ComponentFactory<GDSButton>, GDSButtonComponentFactory>()
            .AddTransient<ComponentFactory<GDSCookieBanner>, GDSCookieBannerComponentFactory>()

            // Commands
            .AddScoped<ICommandHandler<ClickElementCommand>, ClickElementCommandHandler>()
            .AddScoped<ICommandHandler<UpdateElementTextCommand>, UpdateElementTextCommandHandler>()
            // Helpers    
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
