using Dfe.Testing.Pages.Internal.Commands;
using Dfe.Testing.Pages.Internal.DocumentQueryClient.Resolver;

namespace Dfe.Testing.Pages.Internal;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDocumentQueryClient<TProvider>(this IServiceCollection services) where TProvider : class, IDocumentQueryClientProvider
        => services
            // Client API
            .AddScoped<IDocumentClientSession, DocumentClientSession>()
            // Document Query Client
            .AddScoped<IDocumentQueryClientProvider, TProvider>()
            .AddScoped<IDocumentQueryClientAccessor, DocumentQueryClientAccessor>()
            // Page
            .AddScoped<IPageObjectResolver, PageObjectResolver>()
            // Commands
            .AddScoped<ICommandHandler<ClickElementCommand>, ClickElementCommandHandler>()
            .AddScoped<ICommandHandler<UpdateElementTextCommand>, UpdateElementTextCommandHandler>()
            // Helpers
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
