﻿namespace Dfe.Testing.Pages.Public.WebApplicationFactory;
public interface IHttpRequestBuilder
{
    public IHttpRequestBuilder SetDomain(string baseUri);
    public IHttpRequestBuilder SetPath(string path);
    public IHttpRequestBuilder AddQueryParameter(KeyValuePair<string, string> queryParameter);
    public IHttpRequestBuilder SetBody<T>(T value) where T : class;
    public HttpRequestMessage Build();
}
