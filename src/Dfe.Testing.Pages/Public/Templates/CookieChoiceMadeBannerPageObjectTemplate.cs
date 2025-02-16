using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Templates;

public record CookieChoiceMadeBannerComponent
{
    public FormComponent HideCookiesForm { get; init; } = new();
    public AnchorLinkComponent ChangeYourCookieSettingsLink { get; init; } = new();
    public string Content { get; init; } = string.Empty;
}

public sealed class CookieChoiceMadeBannerMapper : IMapper<CreatedPageObjectModel, CookieChoiceMadeBannerComponent>
{
    public CookieChoiceMadeBannerComponent Map(CreatedPageObjectModel input)
    {
        var content = input.GetMappedProperty("Content").Single();

        var formProperties = input.GetMappedProperty("Form").Single();
        var formMethod = formProperties.TryGetOrDefault("method");

        IEnumerable<AnchorLinkComponent> changeYourCookiesLink = input.GetMappedProperty("ChangeCookiesLink").Select(t =>
        {
            return new AnchorLinkComponent()
            {
                Link = t.TryGetOrDefault("href"),
                Text = t.TryGetOrDefault("text"),
                OpensInNewTab = t.TryGetOrDefault("target") == "_blank",
                Rel = t.TryGetOrDefault("rel")
            };
        });

        IEnumerable<ButtonComponent> buttons = input.GetMappedProperty("HideCookiesButton").Select(button =>
        {
            var hideCookiesButtonStyles = button.TryGetOrDefault("class");
            return new ButtonComponent()
            {
                Type = button.TryGetOrDefault("type"),
                Name = button.TryGetOrDefault("name"),
                Value = button.TryGetOrDefault("value"),
                Text = button.TryGetOrDefault("text") ?? string.Empty,
                IsEnabled = button.TryGetOrDefault("disabled") is null,
                ButtonStyle = hideCookiesButtonStyles == null ?
                    ButtonStyleType.Primary :
                    hideCookiesButtonStyles.Contains("govuk-button--secondary") ? ButtonStyleType.Secondary :
                    hideCookiesButtonStyles.Contains("govuk-button--warning") ? ButtonStyleType.Warning
                        : ButtonStyleType.Primary
            };
        });

        return new CookieChoiceMadeBannerComponent()
        {
            HideCookiesForm = new FormComponent()
            {
                Action = formProperties.TryGetOrDefault("action"),
                Method = formMethod == null ? null : HttpMethod.Parse(formMethod),
                Buttons = buttons
            },
            ChangeYourCookieSettingsLink = changeYourCookiesLink.Single(),
            Content = content["text"] ?? string.Empty,
        };
    }
}

public sealed class CookieChoiceMadeBannerPageObjectTemplate : IPageObjectTemplate
{
    public string TemplateId => "CookieChoiceMadeBanner";

    public QueryOptions? Query => new()
    {
        Find = ".govuk-cookie-banner",
    };

    public IEnumerable<PageObjectSchema> PageObjects => [

        new PageObjectSchema()
        {
            Id = "Banner",
            Properties = [
                new PropertyMapping()
                {
                    MappingEntrypoint = "form p",
                    Attributes = ["text"],
                    ToProperty = "Content"
                },
                new PropertyMapping()
                {
                    MappingEntrypoint = "form a",
                    Attributes = ["href", "text", "rel", "id", "target"],
                    ToProperty = "ChangeCookiesLink"
                },
                new PropertyMapping(){
                    MappingEntrypoint = "form",
                    Attributes = ["method", "action"],
                    ToProperty = "Form"
                },
                new PropertyMapping(){
                    MappingEntrypoint = "form button",
                    Attributes = ["id", "type", "text", "class", "name"],
                    ToProperty = "HideCookiesButton"
                },
            ],
            Children = []
        }
    ];
}
