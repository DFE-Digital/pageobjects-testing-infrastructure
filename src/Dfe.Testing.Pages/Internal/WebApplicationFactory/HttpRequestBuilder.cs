using Cookie = System.Net.Cookie;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Internal.WebApplicationFactory;
internal sealed class HttpRequestBuilder : IHttpRequestBuilder
{
    private HttpMethod _method = HttpMethod.Get;
    private string? _domain = "localhost";
    private string _path = "/";
    private readonly List<KeyValuePair<string, string>> _query = [];
    private object? _body = null;
    private ushort _port = 443;
    private Cookie? _cookie;

    public IHttpRequestBuilder SetMethod(string method) => SetMethod(HttpMethod.Parse(method));

    public IHttpRequestBuilder SetMethod(HttpMethod method)
    {
        _method = method;
        return this;
    }

    public IHttpRequestBuilder SetDomain(string domain)
    {
        if (string.IsNullOrEmpty(domain))
        {
            throw new ArgumentException("domain is null or empty");
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

    public IHttpRequestBuilder SetPort(ushort port)
    {
        _port = port;
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

    public IHttpRequestBuilder SetCookie(Cookie cookie)
    {
        ArgumentNullException.ThrowIfNull(cookie);
        _cookie = cookie;
        return this;
    }

    public HttpRequestMessage Build()
    {
        UriBuilder uri = new()
        {
            Path = _path,
            Query = _query.ToList()
                .Aggregate(
                    new StringBuilder(), (init, queryPairs) => init.Append($"{queryPairs.Key}={queryPairs.Value}"))
                .ToString(),
            Port = _port
        };

        // domain and scheme optional as HttpClient could be configured separately
        if (_domain != null)
        {
            uri.Scheme = "https://";
            uri.Host = _domain?.TrimEnd('/') ?? string.Empty;
        }

        HttpRequestMessage requestMessage = new()
        {
            RequestUri = uri.Uri
        };

        if (_body != null)
        {
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(_body));
        }

        if (_cookie != null)
        {
            requestMessage.Headers.Add("Cookie", _cookie.ToString());
        }

        return requestMessage;
    }
}
