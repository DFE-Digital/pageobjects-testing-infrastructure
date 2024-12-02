using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Components.CookieBanner;
using Dfe.Testing.Pages.Components.Details;
using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.ErrorSummary;
using Dfe.Testing.Pages.Components.Fieldset;
using Dfe.Testing.Pages.Components.Footer;
using Dfe.Testing.Pages.Components.Form;
using Dfe.Testing.Pages.Components.Inputs;
using Dfe.Testing.Pages.Components.Inputs.Checkbox;
using Dfe.Testing.Pages.Components.Inputs.Radio;
using Dfe.Testing.Pages.Components.Inputs.TextInput;
using Dfe.Testing.Pages.Components.Label;
using Dfe.Testing.Pages.Components.NotificationBanner;
using Dfe.Testing.Pages.Components.Panel;
using Dfe.Testing.Pages.Components.Select;
using Dfe.Testing.Pages.Components.Table;
using Dfe.Testing.Pages.Components.Tabs;
using Dfe.Testing.Pages.Components.Text;
using Dfe.Testing.Pages.Internal;
using Dfe.Testing.Pages.Public.Mapper;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;
using Dfe.Testing.Pages.Public.Mapper.GDS;
using Dfe.Testing.Pages.Public.Mapper.GDS.Table;
using Dfe.Testing.Pages.Public.Selector.Factory;

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
            .AddSingleton<IComponentSelectorFactory, ComponentSelectorFactory>((sp) =>
            {
                Dictionary<string, Func<IElementSelector>> componentSelectorMapping = new()
                {
                    { nameof(AnchorLinkComponent), () => new CssElementSelector("a")},
                    // may not be approp default if multiple forms on page?
                    { nameof(FormComponent), () => new CssElementSelector("form")},
                    { nameof(LabelComponent), () => new CssElementSelector("label") },
                    { nameof(InputComponent), () => new CssElementSelector("input") },
                    { nameof(GDSHeaderComponent), () => new CssElementSelector(".govuk-header")},
                    { nameof(GDSFieldsetComponent), () => new CssElementSelector("fieldset")},
                    { nameof(GDSCheckboxComponent), () => new CssElementSelector(".govuk-checkboxes__item")},
                    { nameof(GDSRadioComponent), () => new CssElementSelector(".govuk-radios__item") },
                    { nameof(GDSTextInputComponent), () => new CssElementSelector(".govuk-form-group:has(input[type=text])")},
                    { nameof(GDSButtonComponent), () => new CssElementSelector(".govuk-button")},
                    { nameof(GDSCookieBannerComponent), () => new CssElementSelector(".govuk-cookie-banner")},
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
                    { nameof(TableHeading), () => new CssElementSelector("th") },
                    { nameof(TableDataItem), () => new CssElementSelector("td") },
                    { nameof(TextComponent), () => new CssElementSelector("*") },
                    { nameof(TextInputComponent), () => new CssElementSelector("input[type=text]") },
                    { nameof(HiddenInputComponent), () => new CssElementSelector("input[type=hidden]") },
                };
                return new ComponentSelectorFactory(componentSelectorMapping);
            })
        // anchor link
        .AddTransient<ComponentFactory<AnchorLinkComponent>>()
        .AddTransient<IComponentMapper<AnchorLinkComponent>, AnchorLinkMapper>()
        // label
        .AddTransient<ComponentFactory<LabelComponent>>()
        .AddTransient<IComponentMapper<LabelComponent>, LabelMapper>()
        // input
        .AddTransient<ComponentFactory<InputComponent>>()
        .AddTransient<IComponentMapper<InputComponent>, InputMapper>()
        // form
        .AddTransient<ComponentFactory<FormComponent>>()
        .AddTransient<IComponentMapper<FormComponent>, FormMapper>()
        // table
        .AddTransient<ComponentFactory<GDSTableComponent>>()
        .AddTransient<IComponentMapper<GDSTableComponent>, GDSTableMapper>()
        // thead
        .AddTransient<ComponentFactory<TableHead>>()
        .AddTransient<IComponentMapper<TableHead>, TableHeadMapper>()
        // tbody
        .AddTransient<ComponentFactory<TableBody>>()
        .AddTransient<IComponentMapper<TableBody>, TableBodyMapper>()
        // table row
        .AddTransient<ComponentFactory<TableRow>>()
        .AddTransient<IComponentMapper<TableRow>, TableRowMapper>()
        // th
        .AddTransient<ComponentFactory<TableHeading>>()
        .AddTransient<IComponentMapper<TableHeading>, TableHeadingMapper>()
        // td
        .AddTransient<ComponentFactory<TableDataItem>>()
        .AddTransient<IComponentMapper<TableDataItem>, TableDataItemMapper>()
        // button
        .AddTransient<ComponentFactory<GDSButtonComponent>>()
        .AddTransient<IComponentMapper<GDSButtonComponent>, GDSButtonMapper>()
        // header
        .AddTransient<ComponentFactory<GDSHeaderComponent>>()
        .AddTransient<IComponentMapper<GDSHeaderComponent>, GDSHeaderMapper>()
        // fieldset
        .AddTransient<ComponentFactory<GDSFieldsetComponent>>()
        .AddTransient<IComponentMapper<GDSFieldsetComponent>, GDSFieldsetMapper>()
        // checkboxes
        .AddTransient<ComponentFactory<GDSCheckboxComponent>>()
        .AddTransient<IComponentMapper<GDSCheckboxComponent>, GDSCheckboxMapper>()
        // radio
        .AddTransient<ComponentFactory<GDSRadioComponent>>()
        .AddTransient<IComponentMapper<GDSRadioComponent>, GDSRadioMapper>()
        // text input
        .AddTransient<ComponentFactory<GDSTextInputComponent>>()
        .AddTransient<IComponentMapper<GDSTextInputComponent>, GDSTextInputMapper>()
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
        .AddTransient<ComponentFactory<GDSPanelComponent>>()
        // select
        .AddTransient<IComponentMapper<GDSSelectComponent>, GDSSelectMapper>()
        .AddTransient<ComponentFactory<GDSSelectComponent>>()
        // option
        .AddTransient<IComponentMapper<OptionComponent>, OptionsMapper>()
        .AddTransient<ComponentFactory<OptionComponent>>()
        // text
        .AddTransient<IComponentMapper<TextComponent>, TextMapper>()
        .AddTransient<ComponentFactory<TextComponent>>()
        // text input
        .AddTransient<IComponentMapper<TextInputComponent>, TextInputMapper>()
        .AddTransient<ComponentFactory<TextInputComponent>>()
        // hidden input
        .AddTransient<IComponentMapper<HiddenInputComponent>, HiddenInputMapper>()
        .AddTransient<ComponentFactory<HiddenInputComponent>>();
        return services;
    }
}
