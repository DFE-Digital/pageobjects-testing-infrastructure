using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Templates;

public record CookieChoiceAvailableBannerComponent
{
    public string Heading { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public AnchorLinkComponent ViewCookies { get; set; } = new();
    public FormComponent ChoiceForm { get; set; } = new();
}

public sealed class CookieChoiceAvailableBannerMapper : IMapper<CreatedPageObjectModel, CookieChoiceAvailableBannerComponent>
{
    public CookieChoiceAvailableBannerComponent Map(CreatedPageObjectModel input)
    {
        // TODO derive the property keys from options on the templateschema so client can override
        // cookie choice buttons
        var acceptCookiesProperties = input.GetMappedProperty("AcceptCookiesButton").Single();
        var rejectCookiesProperties = input.GetMappedProperty("RejectCookiesButton").Single();
        var rejectCookiesStyles = rejectCookiesProperties.TryGetOrDefault("class");
        var acceptCookiesStyles = acceptCookiesProperties.TryGetOrDefault("class");

        // view cookies
        var viewCookiesProperty = input.GetMappedProperty("ViewCookiesLink").Single();
        // form
        var formProperties = input.GetMappedProperty("Form").Single();
        var formMethod = formProperties.TryGetOrDefault("method");

        return new CookieChoiceAvailableBannerComponent()
        {
            Heading = input.GetMappedProperty("Heading").Single().TryGetOrDefault("text") ?? string.Empty,
            Content = input.GetMappedProperty("Content").Single().TryGetOrDefault("text") ?? string.Empty,
            ViewCookies = new AnchorLinkComponent()
            {
                Link = viewCookiesProperty.TryGetOrDefault("href"),
                Text = viewCookiesProperty.TryGetOrDefault("text"),
                OpensInNewTab = viewCookiesProperty.TryGetOrDefault("target") == "_blank",
                Rel = viewCookiesProperty.TryGetOrDefault("rel")
            },
            ChoiceForm = new FormComponent()
            {
                Method = formMethod is null ? null : HttpMethod.Parse(formMethod),
                Action = formProperties.TryGetOrDefault("action"),
                Buttons =
                [
                    new ButtonComponent()
                    {
                        Type = acceptCookiesProperties.TryGetOrDefault("type"),
                        Name = acceptCookiesProperties.TryGetOrDefault("name"),
                        Value = acceptCookiesProperties.TryGetOrDefault("value"),
                        Text = acceptCookiesProperties.TryGetOrDefault("text") ?? string.Empty,
                        IsEnabled = acceptCookiesProperties.TryGetOrDefault("disabled") is null,
                        ButtonStyle = acceptCookiesStyles == null ? ButtonStyleType.Primary :
                            acceptCookiesStyles.Contains("govuk-button--secondary") ? ButtonStyleType.Secondary :
                            acceptCookiesStyles.Contains("govuk-button--warning") ? ButtonStyleType.Warning
                                : ButtonStyleType.Primary
                    },
                    new ButtonComponent()
                    {
                        Type = rejectCookiesProperties["type"] ?? string.Empty,
                        Name = rejectCookiesProperties["name"] ?? string.Empty,
                        Value = rejectCookiesProperties["value"] ?? string.Empty,
                        Text = rejectCookiesProperties["text"] ?? string.Empty,
                        IsEnabled = rejectCookiesProperties["disabled"] == null,
                        ButtonStyle = rejectCookiesStyles == null ? ButtonStyleType.Primary :
                            rejectCookiesStyles.Contains("govuk-button--secondary") ? ButtonStyleType.Secondary :
                            rejectCookiesStyles.Contains("govuk-button--warning") ? ButtonStyleType.Warning
                                : ButtonStyleType.Primary
                    }
                ]
            }
        };
    }
}

public sealed class CookieChoiceAvailableBannerPageObjectTemplate : IPageObjectTemplate
{
    public string TemplateId => "CookieChoiceAvailableBanner";

    public QueryOptions? Query => new()
    {
        Find = ".govuk-cookie-banner",
    };

    public IEnumerable<PageObjectSchema> PageObjects =>
        [
            new PageObjectSchema()
            {
                Id = "Banner",
                Properties = [
                    new PropertyMapping()
                    {
                        MappingEntrypoint = ".govuk-cookie-banner__heading",
                        Attributes = ["text"],
                        ToProperty = "Heading"
                    },
                    new PropertyMapping()
                    {
                        MappingEntrypoint = ".govuk-cookie-banner__content",
                        Attributes = ["text"],
                        ToProperty = "Content"
                    },
                    new PropertyMapping(){
                        MappingEntrypoint = "form",
                        Attributes = ["method", "action"],
                        ToProperty = "Form"
                    },
                    new PropertyMapping()
                    {
                        MappingEntrypoint = "form button:nth-of-type(1)",
                        Attributes = ["id", "class", "type", "value", "text", "name", "disabled"],
                        ToProperty = "AcceptCookiesButton"
                    },
                    new PropertyMapping()
                    {
                        MappingEntrypoint = "form button:nth-of-type(2)",
                        Attributes = ["id", "class", "type", "value", "text", "name", "disabled"],
                        ToProperty = "RejectCookiesButton"
                    },
                    new PropertyMapping()
                    {
                        MappingEntrypoint = "a",
                        Attributes = ["href", "text", "target", "rel"],
                        ToProperty = "ViewCookiesLink"
                    }
                ]
            }
        ];
}

