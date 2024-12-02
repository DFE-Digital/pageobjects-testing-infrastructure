namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Provider.AngleSharp;
internal sealed class HtmlDocumentProvider : IHtmlDocumentProvider
{
    private readonly HttpClient _client;

    public HtmlDocumentProvider(HttpClient client)
    {
        _client = client;
    }

    public async Task<IHtmlDocument> Get(HttpRequestMessage request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        HttpResponseMessage message = await _client.SendAsync(request);
        return await GetDocumentAsync(message);
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

internal interface IHtmlDocumentProvider
{
    public Task<IHtmlDocument> Get(HttpRequestMessage httpRequestMessage);
}
