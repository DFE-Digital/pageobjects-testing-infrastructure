global using Dfe.Testing.Pages.Internal;
using Dfe.Testing.Pages.Components;
using Dfe.Testing.Pages.Contracts.Documents;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.AngleSharp;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.WebDriver;
using Dfe.Testing.Pages.Public.AngleSharp.Options;
using Microsoft.Extensions.Options;


namespace Dfe.Testing.Pages;

public static class DependencyInjection
{
    public static IServiceCollection AddAngleSharp(this IServiceCollection services, Action<AngleSharpOptions>? configureOptions = null)
    {
        services.AddOptions<AngleSharpOptions>();
        if (configureOptions is not null)
        {
            services.Configure(configureOptions);
        }
        // register the Options type without IOptions wrapper
        services.AddSingleton<AngleSharpOptions>(t => t.GetRequiredService<IOptions<AngleSharpOptions>>().Value);


        services.AddDocumentClientProvider<AngleSharpDocumentClientProvider>();
        services.AddTransient<IHtmlDocumentProvider, HtmlDocumentProvider>();
        services.AddPageObjectTemplates();

        return services;
    }

    public static IServiceCollection AddWebDriver(this IServiceCollection services, Action<WebDriverOptions>? configureOptions = null)
    {
        services.AddOptions<WebDriverOptions>();
        if (configureOptions is not null)
        {
            services.Configure(configureOptions);
        }
        // register the Options type without IOptions wrapper
        services.AddSingleton<WebDriverOptions>(t => t.GetRequiredService<IOptions<WebDriverOptions>>().Value);

        services.AddDocumentClientProvider<WebDriverDocumentClientProvider>();
        services.AddWebDriverServices();
        services.AddPageObjectTemplates();

        return services;
    }

    public static IServiceCollection AddWebApplicationFactory<TApplicationProgram>(this IServiceCollection services) where TApplicationProgram : class
        => services
            .AddScoped<WebApplicationFactory<TApplicationProgram>, TestServerFactory<TApplicationProgram>>()
            // TODO may need to defer the creation of HttpClient
            .AddScoped(scope => scope.GetRequiredService<WebApplicationFactory<TApplicationProgram>>().CreateClient())
            .AddScoped<IConfigureWebHostHandler, ConfigureWebHostHandler>()
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
}
