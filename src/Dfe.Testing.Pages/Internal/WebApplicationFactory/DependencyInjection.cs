namespace Dfe.Testing.Pages.Internal.WebApplicationFactory;

internal static class DependencyInjection
{
    internal static IServiceCollection AddWebApplicationFactory<TApplicationProgram>(this IServiceCollection services) where TApplicationProgram : class
        => services
            .AddScoped<WebApplicationFactory<TApplicationProgram>, TestServerFactory<TApplicationProgram>>()
            .AddScoped(scope => scope.GetRequiredService<WebApplicationFactory<TApplicationProgram>>().CreateClient())
            .AddScoped<IConfigureWebHostHandler, ConfigureWebHostHandler>()
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
