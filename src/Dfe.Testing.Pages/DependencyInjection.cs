using Dfe.Testing.Pages.Internal;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.AngleSharp;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.WebDriver;
using Dfe.Testing.Pages.Public.AngleSharp.Options;
using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Public.Components.Checkbox;
using Dfe.Testing.Pages.Public.Components.EntrypointSelectorFactory;
using Dfe.Testing.Pages.Public.Components.Form;
using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Details;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorSummary;
using Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
using Dfe.Testing.Pages.Public.Components.GDS.Footer;
using Dfe.Testing.Pages.Public.Components.GDS.NotificationBanner;
using Dfe.Testing.Pages.Public.Components.GDS.Panel;
using Dfe.Testing.Pages.Public.Components.GDS.PhaseBanner;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.Table.GDSTable;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableBody;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableDataItem;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableHead;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableHeadingItem;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using Dfe.Testing.Pages.Public.Components.HiddenInput;
using Dfe.Testing.Pages.Public.Components.Label;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Radio;
using Dfe.Testing.Pages.Public.Components.Text;
using Dfe.Testing.Pages.Public.Components.TextInput;
using Dfe.Testing.Pages.Public.Templates;
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
        services.AddComponents();
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
        services.AddComponents();
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


    internal static IServiceCollection AddPageObjectTemplates(this IServiceCollection services)
    {
        services.AddSingleton<IMapper<CreatedPageObjectModel, PhaseBannerComponent>, PhaseBannerComponentMapper>();
        services.AddSingleton<IMapper<CreatedPageObjectModel, CookieChoiceAvailableBannerComponent>, CookieChoiceAvailableBannerMapper>();
        services.AddSingleton<IMapper<CreatedPageObjectModel, CookieChoiceMadeBannerComponent>, CookieChoiceMadeBannerMapper>();
        services.AddSingleton<IMapper<CreatedPageObjectModel, IEnumerable<TabComponent>>, TabComponentMapper>();

        services.AddSingleton<IPageObjectTemplate, CookieChoiceAvailableBannerPageObjectTemplate>();
        services.AddSingleton<IPageObjectTemplate, CookieChoiceMadeBannerPageObjectTemplate>();
        services.AddSingleton<IPageObjectSchemaMerger, PageObjectSchemaMerger>();
        services.AddSingleton<IPageObjectTemplateFactory, PageObjectTemplateFactory>();
        return services;
    }

    internal static IServiceCollection AddComponents(this IServiceCollection services)
    {
        services
            .AddSingleton<IEntrypointSelectorFactory, EntrypointSelectorFactory>((sp) =>
            {
                static IElementSelector getFieldSet() => new CssElementSelector("fieldset");
                static IElementSelector getGdsCheckbox() => new CssElementSelector(".govuk-checkboxes__item");
                static IElementSelector getGdsRadio() => new CssElementSelector(".govuk-radios__item");
                static IElementSelector getGdsTextInput() => new CssElementSelector(".govuk-form-group:has(input[type=text])");
                static IElementSelector getGdsButton() => new CssElementSelector("button");
                static IElementSelector getGdsSelect() => new CssElementSelector(".govuk-form-group:has(select)");

                static IElementSelector getTextInput() => new CssElementSelector("input[type=text]");
                static IElementSelector getHiddenInput() => new CssElementSelector("input[type=hidden]");
                static IElementSelector getCookieBanner() => new CssElementSelector(".govuk-cookie-banner");
                static IElementSelector getRadio() => new CssElementSelector("input[type=radio]");
                static IElementSelector getCheckbox() => new CssElementSelector("input[type=checkbox]");
                static IElementSelector getLink() => new CssElementSelector("a");
                static IElementSelector getForm() => new CssElementSelector("form");

                Dictionary<string, Func<IElementSelector?>> componentDefaultSelectorMapping = new()
                {
                    { "AnchorLinkComponent", getLink},
                    // Form
                    { "FormComponent", getForm },
                    { "FormComponent.Fieldsets", getFieldSet },
                    { "FormComponent.TextInputs", getGdsTextInput },
                    { "FormComponent.Radios", getGdsRadio },
                    { "FormComponent.Checkboxes", getGdsCheckbox },
                    { "FormComponent.HiddenInputs", getHiddenInput },
                    { "FormComponent.Selects" , getGdsSelect },
                    { "FormComponent.Buttons", getGdsButton },
                    { "LabelComponent", () => new CssElementSelector("label") },
                    { "OptionComponent", () => new CssElementSelector("option") },
                    { "TableHeadComponent", () => new CssElementSelector("thead") },
                    { "TableBodyComponent", () => new CssElementSelector("tbody") },
                    { "TableRowComponent", () => new CssElementSelector("tr") },
                    { "TableHeadingItemComponent", () => new CssElementSelector("th") },
                    { "TableDataItemComponent", () => new CssElementSelector("td") },
                    { "TextInputComponent", getTextInput },
                    { "HiddenInputComponent", getHiddenInput },
                    { "RadioComponent", getRadio },
                    { "CheckboxComponent", getCheckbox },
                    { "GDSHeaderComponent", () => new CssElementSelector(".govuk-header")},
                    { "GDSHeaderComponent.GovUKLink", () => new CssElementSelector(".govuk-header__link--homepage")},
                    { "GDSHeaderComponent.NavigationLinks", () => new CssElementSelector("nav a")},
                    { "GDSFieldsetComponent", getFieldSet},
                    { "GDSFieldsetComponent.Legend", () => new CssElementSelector("legend") },
                    { "GDSFieldsetComponent.TextInputs" , getGdsTextInput },
                    { "GDSFieldsetComponent.Radios" , getGdsRadio },
                    { "GDSFieldsetComponent.Checkboxes", getGdsCheckbox },
                    { "GDSCheckboxComponent", getGdsCheckbox},
                    { "GDSRadioComponent", getGdsRadio },
                    { "GDSTextInputComponent", getGdsTextInput},
                    { "GDSButtonComponent", getGdsButton},
                    { "GDSCookieChoiceAvailableBannerComponent", getCookieBanner},
                    { "GDSCookieChoiceAvailableBannerComponent.Heading", () => new CssElementSelector(".govuk-cookie-banner__heading") },
                    { "GDSCookieChoiceAvailableBannerComponent.ViewCookiesLink", getLink  },
                    { "GDSCookieChoiceAvailableBannerComponent.CookieChoiceForm", getForm },
                    { "GDSCookieChoiceAvailableBannerComponent.CookieChoiceForm.Fieldsets", getFieldSet},
                    { "GDSCookieChoiceAvailableBannerComponent.CookieChoiceForm.TextInputs", getGdsTextInput },
                    { "GDSCookieChoiceAvailableBannerComponent.CookieChoiceForm.Radios", getGdsRadio },
                    { "GDSCookieChoiceAvailableBannerComponent.CookieChoiceForm.Checkboxes", getGdsCheckbox },
                    { "GDSCookieChoiceAvailableBannerComponent.CookieChoiceForm.HiddenInputs", getHiddenInput },
                    { "GDSCookieChoiceAvailableBannerComponent.CookieChoiceForm.Selects" , getGdsSelect },
                    { "GDSCookieChoiceAvailableBannerComponent.CookieChoiceForm.Buttons", getGdsButton },
                    { "GDSCookieChoiceMadeBannerComponent", getCookieBanner},
                    { "GDSCookieChoiceMadeBannerComponent.Message", () => new CssElementSelector(".govuk-cookie-banner__content")},
                    { "GDSCookieChoiceMadeBannerComponent.ChangeYourCookieSettingsLink", () => new CssElementSelector(".govuk-cookie-banner__content a")},
                    { "GDSCookieChoiceMadeBannerComponent.HideCookiesForm", getForm },
                    { "GDSCookieChoiceMadeBannerComponent.HideCookiesForm.Fieldsets", getFieldSet},
                    { "GDSCookieChoiceMadeBannerComponent.HideCookiesForm.TextInputs", getGdsTextInput },
                    { "GDSCookieChoiceMadeBannerComponent.HideCookiesForm.Radios", getGdsRadio },
                    { "GDSCookieChoiceMadeBannerComponent.HideCookiesForm.Checkboxes", getGdsCheckbox },
                    { "GDSCookieChoiceMadeBannerComponent.HideCookiesForm.HiddenInputs", getHiddenInput },
                    { "GDSCookieChoiceMadeBannerComponent.HideCookiesForm.Selects" , getGdsSelect },
                    { "GDSCookieChoiceMadeBannerComponent.HideCookiesForm.Buttons", getGdsButton },
                    { "GDSCookieChoiceMadeBannerComponent.Tabs", () => new CssElementSelector(".govuk-cookie-banner_content") },
                    { "GDSTabsComponent", () => new CssElementSelector(".govuk-tabs")},
                    { "GDSTabsComponent.Heading", () => new CssElementSelector(".govuk-tabs__title") },
                    { "GDSTabsComponent.Tabs", () => new CssElementSelector(".govuk-tabs__list") },
                    { "GDSDetailsComponent", () => new CssElementSelector(".govuk-details") },
                    { "GDSDetailsComponent.Summary", () => new CssElementSelector(".govuk-details__summary") },
                    { "GDSDetailsComponent.Content", () => new CssElementSelector(".govuk-details__text") },
                    { "GDSErrorSummaryComponent", () => new CssElementSelector(".govuk-error-summary") },
                    { "GDSErrorSummaryComponent.Heading", () => new CssElementSelector(".govuk-error-summary__title") },
                    { "GDSErrorMessageComponent", () => new CssElementSelector(".govuk-error-message") },
                    { "GDSFooterComponent", () => new CssElementSelector(".govuk-footer") },
                    { "GDSFooterComponent.CrownCopyrightLink", () => new CssElementSelector(".govuk-footer__link .govuk-footer__copyright-logo") },
                    { "GDSFooterComponent.LicenseLink", () => new CssElementSelector(".govuk-footer__licence-description .govuk-footer__link") },
                    { "GDSFooterComponent.LicenseMessage", () => new CssElementSelector(".govuk-footer__licence-description") },
                    { "GDSFooterComponent.ApplicationLinks", () => new CssElementSelector(".govuk-footer__link") },
                    { "GDSSelectComponent", getGdsSelect },
                    { "GDSTableComponent", () => new CssElementSelector(".govuk-table") },
                    { "GDSPhaseBannerComponent", () => new CssElementSelector(".govuk-phase-banner") },
                    { "GDSPhaseBannerComponent.Phase", () => new CssElementSelector(".govuk-tag") },
                    { "GDSPhaseBannerComponent.Text", () => new CssElementSelector(".govuk-phase-banner__text") },
                    { "GDSPhaseBannerComponent.FeedbackLink", getLink },
                    { "GDSNotificationBannerComponent", () => new CssElementSelector(".govuk-notification-banner") },
                    { "GDSNotificationBannerComponent.Heading", () => new CssElementSelector(".govuk-notification-banner__title") },
                    { "GDSNotificationBannerComponent.Content", () => new CssElementSelector(".govuk-notification-banner__content") },
                    { "GDSPanelComponent", () => new CssElementSelector(".govuk-panel") },
                    { "GDSPanelComponent.Heading", () => new CssElementSelector(".govuk-panel__title") },
                    { "GDSPanelComponent.Content", () => new CssElementSelector(".govuk-panel__body") },
                };
                return new EntrypointSelectorFactory(componentDefaultSelectorMapping);
            })
        // open generic so client can use any T
        .AddTransient(typeof(IComponentFactory<>), typeof(ComponentFactory<>))
        .AddSingleton<IMapRequestFactory, MapRequestFactory>()
        // Mappers
        .AddDecoratedMapper<AnchorLinkMapper, AnchorLinkComponentOld>()
        .AddDecoratedMapper<LabelMapper, Public.Components.Label.LabelComponent>()
        .AddDecoratedMapper<FormMapper, FormComponentOld>()
        .AddDecoratedMapper<TableHeadMapper, TableHeadComponent>()
        .AddDecoratedMapper<TableBodyMapper, TableBodyComponent>()
        .AddDecoratedMapper<TableRowMapper, TableRowComponent>()
        .AddDecoratedMapper<TableHeadingItemMapper, TableHeadingItemComponent>()
        .AddDecoratedMapper<TableDataItemMapper, TableDataItemComponent>()
        .AddDecoratedMapper<OptionsMapper, OptionComponent>()
        .AddDecoratedMapper<HiddenInputMapper, HiddenInputComponent>()
        .AddDecoratedMapper<RadioMapper, RadioComponent>()
        .AddDecoratedMapper<TextInputMapper, TextInputComponent>()
        .AddDecoratedMapper<CheckboxMapper, CheckboxComponent>()
        .AddDecoratedMapper<TextMapper, TextComponent>()
        // GDS Mappers
        .AddDecoratedMapper<GDSCookieChoiceAvailableMapper, GDSCookieChoiceAvailableBannerComponent>()
        .AddDecoratedMapper<GDSCookieChoiceMadeBannerComponentMapper, GDSCookieChoiceMadeBannerComponent>()
        .AddDecoratedMapper<GDSButtonMapper, GDSButtonComponent>()
        .AddDecoratedMapper<GDSTableMapper, GDSTableComponent>()
        .AddDecoratedMapper<GDSHeaderMapper, GDSHeaderComponent>()
        .AddDecoratedMapper<GDSFieldsetMapper, GDSFieldsetComponent>()
        .AddDecoratedMapper<GDSCheckboxMapper, GDSCheckboxComponent>()
        .AddDecoratedMapper<GDSRadioMapper, GDSRadioComponent>()
        .AddDecoratedMapper<GDSTextInputMapper, GDSTextInputComponent>()
        .AddDecoratedMapper<GDSTabsMapper, GDSTabsComponent>()
        .AddDecoratedMapper<GDSDetailsMapper, GDSDetailsComponent>()
        .AddDecoratedMapper<GDSErrorMessageMapper, GDSErrorMessageComponent>()
        .AddDecoratedMapper<GDSErrorSummaryMapper, GDSErrorSummaryComponent>()
        .AddDecoratedMapper<GDSFooterMapper, GDSFooterComponent>()
        .AddDecoratedMapper<GDSNotificationBannerMapper, GDSNotificationBannerComponent>()
        .AddDecoratedMapper<GDSPanelMapper, GDSPanelComponent>()
        .AddDecoratedMapper<GDSSelectMapper, GDSSelectComponent>()
        .AddDecoratedMapper<GDSPhaseBannerMapper, GDSPhaseBannerComponent>()
        // Builders for client complex object creation
        .AddTransient<IGDSButtonBuilder, GDSButtonBuilder>()
        .AddTransient<IGDSCheckboxBuilder, GDSCheckboxBuilder>()
        .AddTransient<ICheckboxBuilder, CheckboxBuilder>()
        .AddTransient<IAnchorLinkComponentBuilder, AnchorLinkComponentBuilder>()
        .AddTransient<IGDSCookieChoiceAvailableBannerComponentBuilder, GDSCookieChoiceAvailableBannerComponentBuilder>()
        .AddTransient<IGDSCookieChoiceMadeBannerComponentBuilder, GDSCookieChoiceMadeBannerComponentBuilder>()
        .AddTransient<IGDSPhaseBannerBuilder, GDSPhaseBannerBuilder>()
        .AddTransient<IFormBuilder, FormBuilder>();
        return services;
    }

    private static IServiceCollection AddDecoratedMapper<TDecoratedMapper, TComponent>(this IServiceCollection services)
        where TDecoratedMapper : class, IComponentMapper<TComponent>
        where TComponent : class
    {
        services.AddTransient<TDecoratedMapper>()
            .AddTransient<IComponentMapper<TComponent>>(t =>
            {
                return new SearchDocumentForMappingEntrypointMappingDecorator<TComponent>(
                    t.GetRequiredService<TDecoratedMapper>(),
                    t.GetRequiredService<IMapRequestFactory>());
            });
        return services;
    }
}
