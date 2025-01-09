using Dfe.Testing.Pages.Internal.Commands;
using Dfe.Testing.Pages.Internal.DocumentClient.Options;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Factory;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Options;
using Dfe.Testing.Pages.Public.Components.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;

namespace Dfe.Testing.Pages.Internal;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDocumentClientProvider<TProvider>(this IServiceCollection services) where TProvider : class, IDocumentClientProvider
        => services
            // Client API
            .AddScoped<IDocumentService, DocumentService>()
            .AddSingleton<IMappingResultFactory, MappingResultFactory>()
            .AddSingleton<IMapRequestFactory, MapRequestFactory>()
            // Document Client
            .AddScoped<IDocumentClientProvider, TProvider>()
            // Middleware handlers for GetTextQuery
            .AddSingleton<IGetTextProcessingHandlerFactory, GetTextProcessingHandlerFactory>()
            .AddSingleton<DocumentClientOptions>((options) => new DocumentClientOptions()
            {
                TrimText = true
            })
            .AddSingleton<IMapper<DocumentClientOptions, TextProcessingOptions>, DocumentClientOptionsToTextProcessingOptionsMapper>()

            // Page
            .AddScoped<IPageObjectFactory, PageObjectFactory>()
            // Commands
            .AddScoped<ICommandHandler<ClickElementCommand>, ClickElementCommandHandler>()
            .AddScoped<ICommandHandler<UpdateElementTextCommand>, UpdateElementTextCommandHandler>()
            // Helpers
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>()
            .AddTransient<IGDSButtonBuilder, GDSButtonBuilder>()
            .AddTransient<ICheckboxBuilder, CheckboxBuilder>()
            .AddTransient<IAnchorLinkComponentBuilder, AnchorLinkComponentBuilder>()
            .AddTransient<IGDSCookieChoiceAvailableBannerComponentBuilder, GDSCookieChoiceAvailableBannerComponentBuilder>()
            .AddTransient<IGDSCookieChoiceMadeBannerComponentBuilder, GDSCookieChoiceMadeBannerComponentBuilder>();
}
