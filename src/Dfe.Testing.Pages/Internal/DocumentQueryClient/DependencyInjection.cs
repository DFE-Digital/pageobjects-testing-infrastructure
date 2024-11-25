using Dfe.Testing.Pages.Internal.Components;
using Dfe.Testing.Pages.Internal.Components.AnchorLink;
using Dfe.Testing.Pages.Internal.Components.Button;
using Dfe.Testing.Pages.Internal.Components.Form;
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

            // Components
            // anchor link
            .AddTransient<ComponentFactory<AnchorLinkComponent>, AnchorLinkComponentFactory>()
            .AddTransient<IComponentMapper<AnchorLinkComponent>, AnchorLinkMapper>()
            // form
            .AddTransient<ComponentFactory<FormComponent>, FormFactory>()
            .AddTransient<IComponentMapper<FormComponent>, FormMapper>()
            // header
            .AddTransient<ComponentFactory<GDSHeader>, GDSHeaderFactory>()
            // fieldset
            .AddTransient<ComponentFactory<GDSFieldsetComponent>, GDSFieldsetFactory>()
            .AddTransient<ComponentFactory<GDSCheckboxWithLabel>, GDSCheckboxWithLabelFactory>()
            // button
            .AddTransient<ComponentFactory<GDSButtonComponent>, GDSButtonFactory>()
            .AddTransient<IComponentMapper<GDSButtonComponent>, GDSButtonMapper>()
            .AddTransient<ComponentFactory<GDSCookieBanner>, GDSCookieBannerFactory>()

            // Commands
            .AddScoped<ICommandHandler<ClickElementCommand>, ClickElementCommandHandler>()
            .AddScoped<ICommandHandler<UpdateElementTextCommand>, UpdateElementTextCommandHandler>()
            // Helpers    
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
