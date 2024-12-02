namespace Dfe.Testing.Pages.Internal.WebApplicationFactory;
internal sealed class HttpRequestBuilder : IHttpRequestBuilder
{
    private string? _domain = null;
    private string _path = "/";
    private readonly List<KeyValuePair<string, string>> _query = [];
    private object? _body = null;

    public IHttpRequestBuilder SetDomain(string domain)
    {
        if (string.IsNullOrEmpty(domain))
        {
            throw new ArgumentException("base uri is null or empty");
        }
        _domain = domain;
        return this;
    }

    public IHttpRequestBuilder SetPath(string path)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        _path = path;
        return this;
    }

    public IHttpRequestBuilder AddQueryParameter(KeyValuePair<string, string> query)
    {
        _query.Add(query);
        return this;
    }

    public IHttpRequestBuilder SetBody<T>(T value) where T : class
    {
        ArgumentNullException.ThrowIfNull(value);
        _body = value;
        return this;
    }

    public HttpRequestMessage Build()
    {
        if (string.IsNullOrEmpty(_domain))
        {
            throw new ArgumentNullException("base uri has not been set");
        }

        UriBuilder uri = new()
        {
            Scheme = "https://",
            Host = _domain.TrimEnd('/'),
            Path = _path,
            Query = _query.ToList()
                .Aggregate(
                    new StringBuilder(), (init, queryPairs) => init.Append($"{queryPairs.Key}={queryPairs.Value}"))
                .ToString()
        };

        HttpRequestMessage requestMessage = new()
        {
            RequestUri = uri.Uri
        };

        if (_body != null)
        {
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(_body));
        }

        return requestMessage;
    }
}
