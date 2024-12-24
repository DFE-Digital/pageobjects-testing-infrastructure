using Dfe.Testing.Pages.Internal;
using Dfe.Testing.Pages.Internal.DocumentClient;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.AngleSharp;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.WebDriver;
using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Public.Components.Core.Form;
using Dfe.Testing.Pages.Public.Components.Core.Inputs;
using Dfe.Testing.Pages.Public.Components.Core.Label;
using Dfe.Testing.Pages.Public.Components.Core.Link;
using Dfe.Testing.Pages.Public.Components.Core.Text;
using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Details;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorSummary;
using Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
using Dfe.Testing.Pages.Public.Components.GDS.Footer;
using Dfe.Testing.Pages.Public.Components.GDS.NotificationBanner;
using Dfe.Testing.Pages.Public.Components.GDS.Panel;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages;

public static class DependencyInjection
{
    public static IServiceCollection AddAngleSharp(this IServiceCollection services)
        => services
            .AddDocumentQueryClient<AngleSharpDocumentClientProvider>()
            .AddTransient<IHtmlDocumentProvider, HtmlDocumentProvider>()
            .AddComponentMapping();

    public static IServiceCollection AddWebDriver(this IServiceCollection services)
        => services
            .AddDocumentQueryClient<WebDriverDocumentQueryProvider>()
            .AddWebDriverServices()
            .AddComponentMapping();

    public static IServiceCollection AddWebApplicationFactory<TApplicationProgram>(this IServiceCollection services) where TApplicationProgram : class
        => services
            .AddScoped<WebApplicationFactory<TApplicationProgram>, TestServerFactory<TApplicationProgram>>()
            // TODO may need to defer the creation of HttpClient
            .AddScoped(scope => scope.GetRequiredService<WebApplicationFactory<TApplicationProgram>>().CreateClient())
            .AddScoped<IConfigureWebHostHandler, ConfigureWebHostHandler>()
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();

    internal static IServiceCollection AddComponentMapping(this IServiceCollection services)
    {
        services
            .AddSingleton<IComponentSelectorFactory, ComponentSelectorFactory>((sp) =>
            {
                Dictionary<string, Func<IElementSelector>> componentSelectorMapping = new()
                {
                    { nameof(AnchorLinkComponent), () => new CssElementSelector("a")},
                    // may not be approp default if multiple forms on page?
                    { nameof(FormComponent), () => new CssElementSelector("form")},
                    { nameof(LabelComponent), () => new CssElementSelector("label") },
                    { nameof(GDSHeaderComponent), () => new CssElementSelector(".govuk-header")},
                    { nameof(GDSFieldsetComponent), () => new CssElementSelector("fieldset")},
                    { nameof(GDSCheckboxComponent), () => new CssElementSelector(".govuk-checkboxes__item")},
                    { nameof(GDSRadioComponent), () => new CssElementSelector(".govuk-radios__item") },
                    { nameof(GDSTextInputComponent), () => new CssElementSelector(".govuk-form-group:has(input[type=text])")},
                    { nameof(GDSButtonComponent), () => new CssElementSelector("button")},
                    { nameof(GDSCookieChoiceAvailableBannerComponent), () => new CssElementSelector(".govuk-cookie-banner")},
                    { nameof(GDSCookieChoiceMadeBannerComponent), () => new CssElementSelector(".govuk-cookie-banner")},
                    { nameof(GDSTabsComponent), () => new CssElementSelector(".govuk-tabs")},
                    { nameof(GDSDetailsComponent), () => new CssElementSelector(".govuk-details") },
                    { nameof(GDSErrorSummaryComponent), () => new CssElementSelector(".govuk-error-summary") },
                    { nameof(GDSErrorMessageComponent), () => new CssElementSelector(".govuk-error-message") },
                    { nameof(GDSFooterComponent), () => new CssElementSelector(".govuk-footer") },
                    { nameof(GDSNotificationBannerComponent), () => new CssElementSelector(".govuk-notification-banner") },
                    { nameof(GDSPanelComponent), () => new CssElementSelector(".govuk-panel") },
                    { nameof(GDSSelectComponent), () => new CssElementSelector(".govuk-form-group:has(select)") },
                    { nameof(OptionComponent), () => new CssElementSelector("option") },
                    { nameof(GDSTableComponent), () => new CssElementSelector(".govuk-table") },
                    { nameof(TableHead), () => new CssElementSelector("thead") },
                    { nameof(TableBody), () => new CssElementSelector("tbody") },
                    { nameof(TableRow), () => new CssElementSelector("tr") },
                    { nameof(TableHeadingItem), () => new CssElementSelector("th") },
                    { nameof(TableDataItem), () => new CssElementSelector("td") },
                    { nameof(TextComponent), () => new CssElementSelector("*") },
                    { nameof(TextInputComponent), () => new CssElementSelector("input[type=text]") },
                    { nameof(HiddenInputComponent), () => new CssElementSelector("input[type=hidden]") },
                    { nameof(RadioComponent), () => new CssElementSelector("input[type=radio]") },
                    { nameof(CheckboxComponent), () => new CssElementSelector("input[type=checkbox]") },
                };
                return new ComponentSelectorFactory(componentSelectorMapping);
            })
        .AddSingleton<IDocumentSectionFinder, DocumentSectionFinder>()
        // anchor link
        .AddTransient<ComponentFactory<AnchorLinkComponent>>()
        .AddTransient<IMapper<IDocumentSection, AnchorLinkComponent>, AnchorLinkMapper>()
        // label
        .AddTransient<ComponentFactory<LabelComponent>>()
        .AddTransient<IMapper<IDocumentSection, LabelComponent>, LabelMapper>()
        // form
        .AddTransient<ComponentFactory<FormComponent>>()
        .AddTransient<IMapper<IDocumentSection, FormComponent>, FormMapper>()
        // table
        .AddTransient<ComponentFactory<GDSTableComponent>>()
        .AddTransient<IMapper<IDocumentSection, GDSTableComponent>, GDSTableMapper>()
        // thead
        .AddTransient<ComponentFactory<TableHead>>()
        .AddTransient<IMapper<IDocumentSection, TableHead>, TableHeadMapper>()
        // tbody
        .AddTransient<ComponentFactory<TableBody>>()
        .AddTransient<IMapper<IDocumentSection, TableBody>, TableBodyMapper>()
        // table row
        .AddTransient<ComponentFactory<TableRow>>()
        .AddTransient<IMapper<IDocumentSection, TableRow>, TableRowMapper>()
        // th
        .AddTransient<ComponentFactory<TableHeadingItem>>()
        .AddTransient<IMapper<IDocumentSection, TableHeadingItem>, TableHeadingMapper>()
        // td
        .AddTransient<ComponentFactory<TableDataItem>>()
        .AddTransient<IMapper<IDocumentSection, TableDataItem>, TableDataItemMapper>()
        // button
        .AddTransient<ComponentFactory<GDSButtonComponent>>()
        .AddTransient<IMapper<IDocumentSection, GDSButtonComponent>, GDSButtonMapper>()
        // header
        .AddTransient<ComponentFactory<GDSHeaderComponent>>()
        .AddTransient<IMapper<IDocumentSection, GDSHeaderComponent>, GDSHeaderMapper>()
        // fieldset
        .AddTransient<ComponentFactory<GDSFieldsetComponent>>()
        .AddTransient<IMapper<IDocumentSection, GDSFieldsetComponent>, GDSFieldsetMapper>()
        // checkboxes
        .AddTransient<ComponentFactory<GDSCheckboxComponent>>()
        .AddTransient<IMapper<IDocumentSection, GDSCheckboxComponent>, GDSCheckboxMapper>()
        // radio
        .AddTransient<ComponentFactory<GDSRadioComponent>>()
        .AddTransient<IMapper<IDocumentSection, GDSRadioComponent>, GDSRadioMapper>()
        // text input
        .AddTransient<ComponentFactory<GDSTextInputComponent>>()
        .AddTransient<IMapper<IDocumentSection, GDSTextInputComponent>, GDSTextInputMapper>()
        // cookie choice banner
        .AddTransient<ComponentFactory<GDSCookieChoiceAvailableBannerComponent>>()
        .AddTransient<IMapper<IDocumentSection, GDSCookieChoiceAvailableBannerComponent>, GDSCookieChoiceAvailableMapper>()
        // cookie choice made banner
        .AddTransient<ComponentFactory<GDSCookieChoiceMadeBannerComponent>>()
        .AddTransient<IMapper<IDocumentSection, GDSCookieChoiceMadeBannerComponent>, GDSCookieChoiceMadeBannerMappper>()
        // tabs
        .AddTransient<ComponentFactory<GDSTabsComponent>>()
        .AddTransient<IMapper<IDocumentSection, GDSTabsComponent>, GDSTabsMapper>()
        // details
        .AddTransient<IMapper<IDocumentSection, GDSDetailsComponent>, GDSDetailsMapper>()
        .AddTransient<ComponentFactory<GDSDetailsComponent>>()
        // error message
        .AddTransient<IMapper<IDocumentSection, GDSErrorMessageComponent>, GDSErrorMessageMapper>()
        .AddTransient<ComponentFactory<GDSErrorMessageComponent>>()
        // error summary
        .AddTransient<IMapper<IDocumentSection, GDSErrorSummaryComponent>, GDSErrorSummaryMapper>()
        .AddTransient<ComponentFactory<GDSErrorSummaryComponent>>()
        // footer
        .AddTransient<IMapper<IDocumentSection, GDSFooterComponent>, GDSFooterMapper>()
        .AddTransient<ComponentFactory<GDSFooterComponent>>()
        // notification banner
        .AddTransient<IMapper<IDocumentSection, GDSNotificationBannerComponent>, GDSNotificationBannerMapper>()
        .AddTransient<ComponentFactory<GDSNotificationBannerComponent>>()
        // panel
        .AddTransient<IMapper<IDocumentSection, GDSPanelComponent>, GDSPanelMapper>()
        .AddTransient<ComponentFactory<GDSPanelComponent>>()
        // select
        .AddTransient<IMapper<IDocumentSection, GDSSelectComponent>, GDSSelectMapper>()
        .AddTransient<ComponentFactory<GDSSelectComponent>>()
        // option
        .AddTransient<IMapper<IDocumentSection, OptionComponent>, OptionsMapper>()
        .AddTransient<ComponentFactory<OptionComponent>>()
        // text
        .AddTransient<IMapper<IDocumentSection, TextComponent>, TextMapper>()
        .AddTransient<ComponentFactory<TextComponent>>()
        // text input
        .AddTransient<IMapper<IDocumentSection, TextInputComponent>, TextInputMapper>()
        .AddTransient<ComponentFactory<TextInputComponent>>()
        // hidden input
        .AddTransient<IMapper<IDocumentSection, HiddenInputComponent>, HiddenInputMapper>()
        .AddTransient<ComponentFactory<HiddenInputComponent>>()
        // radio
        .AddTransient<IMapper<IDocumentSection, RadioComponent>, RadioMapper>()
        .AddTransient<ComponentFactory<RadioComponent>>()
        // checkbox
        .AddTransient<IMapper<IDocumentSection, CheckboxComponent>, CheckboxMapper>()
        .AddTransient<ComponentFactory<CheckboxComponent>>();
        return services;
    }
}
