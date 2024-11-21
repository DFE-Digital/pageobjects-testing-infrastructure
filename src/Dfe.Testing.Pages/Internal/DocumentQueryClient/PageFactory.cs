﻿namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;
// TODO service locator anti-pattern does it corrupt lifecycle management?
internal sealed class PageFactory : IPageFactory
{
    private readonly IServiceProvider _provider;
    private readonly IDocumentQueryClientProvider _documentQueryClientProvider;

    public PageFactory(
        IServiceProvider provider,
        IDocumentQueryClientProvider documentClientFactory)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(documentClientFactory);
        _provider = provider;
        _documentQueryClientProvider = documentClientFactory;
    }

    public async Task<TPage> CreatePageAsync<TPage>(HttpRequestMessage httpRequestMessage) where TPage : class
    {
        var documentClient = await _documentQueryClientProvider.CreateDocumentClientAsync(httpRequestMessage);
        ArgumentNullException.ThrowIfNull(documentClient);

        // add IDocumentQueryClient into the accessor as components need to be able to resolve the same client within same scope
        var accessor = _provider.GetRequiredService<IDocumentQueryClientAccessor>();
        ArgumentNullException.ThrowIfNull(accessor);
        accessor.DocumentQueryClient = documentClient;

        return _provider.GetRequiredService<TPage>();
    }
}
