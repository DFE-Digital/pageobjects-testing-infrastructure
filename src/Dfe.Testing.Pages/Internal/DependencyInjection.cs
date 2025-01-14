using Dfe.Testing.Pages.Internal.Commands;
using Dfe.Testing.Pages.Internal.DocumentClient.Options;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Factory;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Options;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;

namespace Dfe.Testing.Pages.Internal;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDocumentClientProvider<TProvider>(this IServiceCollection services) where TProvider : class, IDocumentClientProvider
        => services

            .AddSingleton<IMappingResultFactory, MappingResultFactory>()
            .AddSingleton<IMapRequestFactory, MapRequestFactory>()
            .AddSingleton<IMapper<DocumentClientOptions, TextProcessingOptions>, DocumentClientOptionsToTextProcessingOptionsMapper>()
            // Client API
            .AddScoped<IDocumentService, DocumentService>()
            .AddScoped<IDocumentClientProvider, TProvider>()
            // Pages
            // TODO currently needs to be scoped due to dep on DocService
            .AddScoped<IPageObjectFactory, PageObjectFactory>()
            // Middleware handlers for GetTextQuery
            .AddSingleton<IGetTextProcessingHandlerFactory, GetTextProcessingHandlerFactory>()
            .AddSingleton<DocumentClientOptions>((options) => new DocumentClientOptions()
            {
                TrimText = true
            })
            // Command handlers
            .AddTransient<ICommandHandler<ClickElementCommand>, ClickElementCommandHandler>()
            .AddTransient<ICommandHandler<UpdateElementTextCommand>, UpdateElementTextCommandHandler>()
            // Helpers
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
