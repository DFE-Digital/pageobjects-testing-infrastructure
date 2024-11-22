﻿namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Provider.AngleSharp;
internal class AngleSharpDocumentQueryClientProvider : IDocumentQueryClientProvider
{
    private readonly HttpClient _client;

    public AngleSharpDocumentQueryClientProvider(HttpClient httpClient)
    {
        _client = httpClient;
    }
    public async Task<IDocumentQueryClient> CreateDocumentClientAsync(HttpRequestMessage httpRequestMessage)
    {
        var httpResponseMessage = await _client.SendAsync(httpRequestMessage);
        var document = await GetDocumentAsync(httpResponseMessage);
        return new AngleSharpDocumentQueryClient(document);
    }

    private static async Task<IHtmlDocument> GetDocumentAsync(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var config = Configuration.Default
            .WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true })
            .WithCss();

        var document = await
            BrowsingContext.New(config)
                    .OpenAsync(ResponseFactory, CancellationToken.None);

        return (IHtmlDocument)document;
        void ResponseFactory(VirtualResponse htmlResponse)
        {
            htmlResponse
                .Address(response.RequestMessage!.RequestUri)
                .Status(response.StatusCode);

            MapHeaders(response.Headers);
            MapHeaders(response.Content.Headers);
            htmlResponse.Content(content);
            void MapHeaders(HttpHeaders headers)
            {
                foreach (var header in headers)
                {
                    foreach (var value in header.Value)
                    {
                        htmlResponse.Header(header.Key, value);
                    }
                }
            }
        }
    }
}
