using Dfe.Testing.Pages.Internal.Commands;
using Dfe.Testing.Pages.Internal.DocumentClient;
using Dfe.Testing.Pages.Internal.DocumentClient.Options;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Factory;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Options;

namespace Dfe.Testing.Pages.Internal;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDocumentQueryClient<TProvider>(this IServiceCollection services) where TProvider : class, IDocumentClientProvider
        => services
            // Client API
            .AddScoped<IDocumentService, DocumentService>()
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
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
