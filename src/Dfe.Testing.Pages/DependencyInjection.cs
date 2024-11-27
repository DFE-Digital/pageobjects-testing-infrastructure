using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Components.Checkbox;
using Dfe.Testing.Pages.Components.CookieBanner;
using Dfe.Testing.Pages.Components.Details;
using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.ErrorSummary;
using Dfe.Testing.Pages.Components.Fieldset;
using Dfe.Testing.Pages.Components.Footer;
using Dfe.Testing.Pages.Components.Form;
using Dfe.Testing.Pages.Components.NotificationBanner;
using Dfe.Testing.Pages.Components.Panel;
using Dfe.Testing.Pages.Components.Tabs;
using Dfe.Testing.Pages.Components.TextInput;
using Dfe.Testing.Pages.Internal;
using Dfe.Testing.Pages.Public.Mapper;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages;

public static class DependencyInjection
{
    public static IServiceCollection AddAngleSharp<TApplicationProgram>(this IServiceCollection services) where TApplicationProgram : class
        => services
            .AddDocumentQueryClient<AngleSharpDocumentQueryClientProvider>()
            .AddWebApplicationFactory<TApplicationProgram>()
            .AddComponentMapping();

    public static IServiceCollection AddWebDriver(this IServiceCollection services)
        => services
            .AddDocumentQueryClient<WebDriverDocumentQueryClientProvider>()
            .AddWebDriverServices()
            .AddComponentMapping();

    internal static IServiceCollection AddComponentMapping(this IServiceCollection services)
    {
        services
            .AddSingleton<IComponentDefaultSelectorFactory, ComponentSelectorFactory>((sp) =>
            {
                Dictionary<string, Func<IElementSelector>> componentSelectorMapping = new()
                {
                    { nameof(AnchorLinkComponent), () => new CssSelector("a")},
                    { nameof(GDSHeaderComponent), () => new CssSelector(".govuk-header")},
                    { nameof(GDSFieldsetComponent), () => new CssSelector("fieldset")},
                    { nameof(GDSCheckboxComponent), () => new CssSelector(".govuk-checkboxes__item")},
                    { nameof(GDSButtonComponent), () => new CssSelector(".govuk-button")},
                    { nameof(GDSTextInputComponent), () => new CssSelector(".govuk-form-group:has(input[type=text])")},
                    { nameof(GDSCookieBannerComponent), () => new CssSelector(".govuk-cookie-banner")},
                    // may not be approp default if multiple forms on page?
                    { nameof(FormComponent), () => new CssSelector("form")},
                    { nameof(GDSTabsComponent), () => new CssSelector(".govuk-tabs")},
                    { nameof(GDSDetailsComponent), () => new CssSelector(".govuk-details") },
                    { nameof(GDSErrorSummaryComponent), () => new CssSelector(".govuk-error-summary") },
                    { nameof(GDSErrorMessageComponent), () => new CssSelector(".govuk-error-message") },
                    { nameof(GDSFooterComponent), () => new CssSelector(".govuk-footer") },
                    { nameof(GDSNotificationBannerComponent), () => new CssSelector(".govuk-notification-banner") },
                    { nameof(GDSPanelMapper), () => new CssSelector(".govuk-panel") }
                };

                return new ComponentSelectorFactory(componentSelectorMapping);
            })
        // anchor link
        .AddTransient<ComponentFactory<AnchorLinkComponent>>()
        .AddTransient<IComponentMapper<AnchorLinkComponent>, AnchorLinkMapper>()
        // button
        .AddTransient<ComponentFactory<GDSButtonComponent>>()
        .AddTransient<IComponentMapper<GDSButtonComponent>, GDSButtonMapper>()
        // text input
        .AddTransient<ComponentFactory<GDSTextInputComponent>>()
        .AddTransient<IComponentMapper<GDSTextInputComponent>, GDSTextInputMapper>()
        // form
        .AddTransient<ComponentFactory<FormComponent>>()
        .AddTransient<IComponentMapper<FormComponent>, FormMapper>()
        // header
        .AddTransient<ComponentFactory<GDSHeaderComponent>>()
        .AddTransient<IComponentMapper<GDSHeaderComponent>, GDSHeaderMapper>()
        // fieldset
        .AddTransient<ComponentFactory<GDSFieldsetComponent>>()
        .AddTransient<IComponentMapper<GDSFieldsetComponent>, GDSFieldsetMapper>()
        // checkboxes
        .AddTransient<ComponentFactory<GDSCheckboxComponent>>()
        .AddTransient<IComponentMapper<GDSCheckboxComponent>, GDSCheckboxMapper>()
        // cookie banner
        .AddTransient<ComponentFactory<GDSCookieBannerComponent>>()
        .AddTransient<IComponentMapper<GDSCookieBannerComponent>, GDSCookieBannerMapper>()
        // tabs
        .AddTransient<ComponentFactory<GDSTabsComponent>>()
        .AddTransient<IComponentMapper<GDSTabsComponent>, GDSTabsMapper>()
        // details
        .AddTransient<IComponentMapper<GDSDetailsComponent>, GDSDetailsMapper>()
        .AddTransient<ComponentFactory<GDSDetailsComponent>>()
        // error message
        .AddTransient<IComponentMapper<GDSErrorMessageComponent>, GDSErrorMessageMapper>()
        .AddTransient<ComponentFactory<GDSErrorMessageComponent>>()
        // error summary
        .AddTransient<IComponentMapper<GDSErrorSummaryComponent>, GDSErrorSummaryMapper>()
        .AddTransient<ComponentFactory<GDSErrorSummaryComponent>>()
        // footer
        .AddTransient<IComponentMapper<GDSFooterComponent>, GDSFooterMapper>()
        .AddTransient<ComponentFactory<GDSFooterComponent>>()
        // notification banner
        .AddTransient<IComponentMapper<GDSNotificationBannerComponent>, GDSNotificationBannerMapper>()
        .AddTransient<ComponentFactory<GDSNotificationBannerComponent>>()
        // panel
        .AddTransient<IComponentMapper<GDSPanelComponent>, GDSPanelMapper>()
        .AddTransient<ComponentFactory<GDSPanelComponent>>();
        return services;
    }
}
