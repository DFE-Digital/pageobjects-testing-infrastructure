using Dfe.Testing.Pages.Internal;
using Dfe.Testing.Pages.Internal.DocumentClient.Options;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.AngleSharp;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.WebDriver;
using Dfe.Testing.Pages.Public.AngleSharp.Options;
using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Public.Components.Checkbox;
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
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.Table.GDSTable;
using Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableBody;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableDataItem;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableHead;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableHeadingItem;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using Dfe.Testing.Pages.Public.Components.HiddenInput;
using Dfe.Testing.Pages.Public.Components.Inputs;
using Dfe.Testing.Pages.Public.Components.Label;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Radio;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;
using Dfe.Testing.Pages.Public.Components.Text;
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


        services
            .AddDocumentClientProvider<AngleSharpDocumentClientProvider>()
            .AddTransient<IHtmlDocumentProvider, HtmlDocumentProvider>()
            .AddComponentMappings();

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


        services
            .AddDocumentClientProvider<WebDriverDocumentClientProvider>()
            .AddWebDriverServices()
            .AddComponentMappings();

        return services;
    }

    public static IServiceCollection AddWebApplicationFactory<TApplicationProgram>(this IServiceCollection services) where TApplicationProgram : class
        => services
            .AddScoped<WebApplicationFactory<TApplicationProgram>, TestServerFactory<TApplicationProgram>>()
            // TODO may need to defer the creation of HttpClient
            .AddScoped(scope => scope.GetRequiredService<WebApplicationFactory<TApplicationProgram>>().CreateClient())
            .AddScoped<IConfigureWebHostHandler, ConfigureWebHostHandler>()
            .AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();

    internal static IServiceCollection AddComponentMappings(this IServiceCollection services)
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
                    { nameof(TableHeadComponent), () => new CssElementSelector("thead") },
                    { nameof(TableBodyComponent), () => new CssElementSelector("tbody") },
                    { nameof(TableRowComponent), () => new CssElementSelector("tr") },
                    { nameof(TableHeadingItemComponent), () => new CssElementSelector("th") },
                    { nameof(TableDataItemComponent), () => new CssElementSelector("td") },
                    { nameof(TextComponent), () => new CssElementSelector("*") },
                    { nameof(TextInputComponent), () => new CssElementSelector("input[type=text]") },
                    { nameof(HiddenInputComponent), () => new CssElementSelector("input[type=hidden]") },
                    { nameof(RadioComponent), () => new CssElementSelector("input[type=radio]") },
                    { nameof(CheckboxComponent), () => new CssElementSelector("input[type=checkbox]") },
                };
                return new ComponentSelectorFactory(componentSelectorMapping);
            })
        .AddComponentMapper<AnchorLinkComponent, AnchorLinkMapper>()
        .AddComponentMapper<LabelComponent, LabelMapper>()
        .AddComponentMapper<FormComponent, FormMapper>()
        .AddComponentMapper<TableHeadComponent, TableHeadMapper>()
        .AddComponentMapper<TableBodyComponent, TableBodyMapper>()
        .AddComponentMapper<TableRowComponent, TableRowMapper>()
        .AddComponentMapper<TableHeadingItemComponent, TableHeadingItemMapper>()
        .AddComponentMapper<TableDataItemComponent, TableDataItemMapper>()
        .AddComponentMapper<OptionComponent, OptionsMapper>()
        .AddComponentMapper<HiddenInputComponent, HiddenInputMapper>()
        .AddComponentMapper<RadioComponent, RadioMapper>()
        .AddComponentMapper<TextInputComponent, TextInputMapper>()
        .AddComponentMapper<CheckboxComponent, CheckboxMapper>()
        .AddComponentMapper<GDSCookieChoiceAvailableBannerComponent, GDSCookieChoiceAvailableMapper>()
        .AddComponentMapper<GDSCookieChoiceMadeBannerComponent, GDSCookieChoiceMadeBannerMappper>()
        .AddComponentMapper<TextComponent, TextMapper>()
        // GDS
        .AddComponentMapper<GDSButtonComponent, GDSButtonMapper>()
        .AddComponentMapper<GDSTableComponent, GDSTableMapper>()
        .AddComponentMapper<GDSHeaderComponent, GDSHeaderMapper>()
        .AddComponentMapper<GDSFieldsetComponent, GDSFieldsetMapper>()
        .AddComponentMapper<GDSCheckboxComponent, GDSCheckboxMapper>()
        .AddComponentMapper<GDSRadioComponent, GDSRadioMapper>()
        .AddComponentMapper<GDSTextInputComponent, GDSTextInputMapper>()
        .AddComponentMapper<GDSTabsComponent, GDSTabsMapper>()
        .AddComponentMapper<GDSDetailsComponent, GDSDetailsMapper>()
        .AddComponentMapper<GDSErrorMessageComponent, GDSErrorMessageMapper>()
        .AddComponentMapper<GDSErrorSummaryComponent, GDSErrorSummaryMapper>()
        .AddComponentMapper<GDSFooterComponent, GDSFooterMapper>()
        .AddComponentMapper<GDSNotificationBannerComponent, GDSNotificationBannerMapper>()
        .AddComponentMapper<GDSPanelComponent, GDSPanelMapper>()
        .AddComponentMapper<GDSSelectComponent, GDSSelectMapper>()
        // Builders for client to create complex objects
        .AddTransient<IGDSButtonBuilder, GDSButtonBuilder>()
        .AddTransient<IGDSCheckboxBuilder, GDSCheckboxBuilder>()
        .AddTransient<ICheckboxBuilder, CheckboxBuilder>()
        .AddTransient<IAnchorLinkComponentBuilder, AnchorLinkComponentBuilder>()
        .AddTransient<IGDSCookieChoiceAvailableBannerComponentBuilder, GDSCookieChoiceAvailableBannerComponentBuilder>()
        .AddTransient<IGDSCookieChoiceMadeBannerComponentBuilder, GDSCookieChoiceMadeBannerComponentBuilder>();
        return services;
    }
}

internal static class ComponentExtensions
{
    public static IServiceCollection AddComponentMapper<TComponent, TMapperImpl>(this IServiceCollection services)
        where TComponent : class
        where TMapperImpl : class, IMapper<IMapRequest<IDocumentSection>, MappedResponse<TComponent>>
    {
        services.AddTransient<ComponentFactory<TComponent>>()
        // decorated mapper
            .AddTransient<TMapperImpl>()
            .AddTransient<IMapper<IMapRequest<IDocumentSection>, MappedResponse<TComponent>>>((serviceProvider) =>
                    new FindMappingEntrypointFromDocumentSectionMapperDecorator<TComponent>(
                        mapRequestFactory: serviceProvider.GetRequiredService<IMapRequestFactory>(),
                        decoratedMapper: serviceProvider.GetRequiredService<TMapperImpl>(),
                        componentSelectorFactory: serviceProvider.GetRequiredService<IComponentSelectorFactory>(),
                        mappingResultFactory: serviceProvider.GetRequiredService<IMappingResultFactory>()));
        return services;
    }
}
