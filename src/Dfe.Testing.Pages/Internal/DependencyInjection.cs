using Dfe.Testing.Pages.Internal.Commands;
using Dfe.Testing.Pages.Public.Commands;

namespace Dfe.Testing.Pages.Internal;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDocumentQueryClient<TProvider>(this IServiceCollection services) where TProvider : class, IDocumentQueryClientProvider
        => services
            .AddScoped<IDocumentQueryClientProvider, TProvider>()
            .AddScoped<IDocumentQueryClientAccessor, DocumentQueryClientAccessor>()
            // Page
            .AddScoped<IPageFactory, PageFactory>()
            // Commands
            .AddScoped<ICommandHandler<ClickElementCommand>, ClickElementCommandHandler>()
            .AddScoped<ICommandHandler<UpdateElementTextCommand>, UpdateElementTextCommandHandler>()
            // Helpers
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
