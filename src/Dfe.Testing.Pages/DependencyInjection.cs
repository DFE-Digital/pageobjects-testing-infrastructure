global using Dfe.Testing.Pages.Internal;
using Dfe.Testing.Pages.Components;
using Dfe.Testing.Pages.Components.AnchorLink;
using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Components.CookieChoiceBanner;
using Dfe.Testing.Pages.Components.Form;
using Dfe.Testing.Pages.Components.Input;
using Dfe.Testing.Pages.Components.Phase;
using Dfe.Testing.Pages.Components.Tab;
using Dfe.Testing.Pages.Contracts.Documents;
using Dfe.Testing.Pages.Contracts.PageObjectClient.Response;
using Dfe.Testing.Pages.Contracts.PageObjectClient.Templates;
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

    private static IServiceCollection AddPageObjectTemplates(this IServiceCollection services)
    {
        // cookie choice
        services.AddSingleton<IPageObjectTemplate, CookieChoiceAvailableBannerPageObjectTemplate>();
        services.AddSingleton<IPageObjectTemplate, CookieChoiceMadeBannerPageObjectTemplate>();
        services.AddSingleton<IMapper<PageObjectResponse, CookieChoiceAvailableBannerComponent>, CookieChoiceAvailableBannerPageObjectResponseMapper>();
        services.AddSingleton<IMapper<PageObjectResponse, CookieChoiceMadeBannerComponent>, CookieChoiceMadeBannerMapper>();
        services.AddSingleton<CookieChoiceAvailableBannerPropertyOptions>();
        services.AddSingleton<CookieChoiceMadeBannerPropertyOptions>();

        // phase banner
        services.AddSingleton<IMapper<PageObjectResponse, PhaseBannerComponent>, PhaseBannerComponentMapper>();
        services.AddSingleton<IPageObjectTemplate, PhaseBannerComponentTemplate>();
        services.AddSingleton<PhaseBannerPageObjectPropertyOptions>();

        // tabs
        services.AddSingleton<IMapper<PageObjectResponse, GdsTabsComponent>, GdsTabMapper>();
        services.AddSingleton<IMapper<CreatedPageObjectModel, ButtonComponent>, ButtonComponentMapper>();
        services.AddSingleton<IMapper<CreatedPageObjectModel, AnchorLinkComponent>, AnchorLinkComponentMapper>();

        // form
        services.AddSingleton<IMapper<CreatedPageObjectModel, FormComponent>, FormNewMapper>();
        services.AddSingleton<FormTemplate>();
        services.AddSingleton<IPageObjectTemplate, FormTemplate>(sp => sp.GetRequiredService<FormTemplate>());
        services.AddSingleton<FormPageOptions>();

        // input
        services.AddSingleton<IMapper<CreatedPageObjectModel, InputComponent>, InputMapper>();
        services.AddSingleton<InputComponentOptions>();
        services.AddSingleton<IPageObjectTemplate, InputComponentTemplate>();

        // tabs
        services.AddSingleton<IPageObjectTemplate, GdsTabsComponentTemplate>();
        services.AddSingleton<GdsTabsOptions>();
        services.AddSingleton<GdsTabsComponentTemplate>();

        services.AddContracts();
        return services;
    }

}

