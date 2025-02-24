using Dfe.Testing.Pages.Contracts.Documents;
using Dfe.Testing.Pages.Contracts.PageObjectClient;
using Dfe.Testing.Pages.Internal.Commands;
using Dfe.Testing.Pages.Internal.DocumentClient.Options;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Factory;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Options;
using Dfe.Testing.Pages.Public.Commands;

namespace Dfe.Testing.Pages.Internal;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDocumentClientProvider<TProvider>(this IServiceCollection services) where TProvider : class, IDocumentClientProvider
        => services
            .AddSingleton<IMapper<DocumentClientOptions, TextProcessingOptions>, DocumentClientOptionsToTextProcessingOptionsMapper>()
            // Client API
            .AddScoped<IDocumentService, DocumentService>()
            .AddScoped<IPageObjectClient, PageObjectClient>()
            .AddScoped<IDocumentClientProvider, TProvider>()
            // Pages
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
