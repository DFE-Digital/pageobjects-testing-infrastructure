using Dfe.Testing.Pages.Components.AnchorLink;
using Dfe.Testing.Pages.Components.Input;
using Dfe.Testing.Pages.Contracts.PageObjectClient.Response;
using Dfe.Testing.Pages.Contracts.PageObjectClient.Templates;

namespace Dfe.Testing.Pages.Components;
public static class DependencyInjection
{
    public static IServiceCollection AddPageObjectTemplates(this IServiceCollection services)
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
