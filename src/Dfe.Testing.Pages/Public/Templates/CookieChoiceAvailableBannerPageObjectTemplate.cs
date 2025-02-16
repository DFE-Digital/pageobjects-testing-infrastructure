namespace Dfe.Testing.Pages.Public.Templates;
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
                Mapping = [
                    new AttributeMapping()
                    {
                        MappingEntrypoint = ".govuk-cookie-banner__heading",
                        Attributes = ["text"],
                        ToProperty = "Heading"
                    },
                    new AttributeMapping()
                    {
                        MappingEntrypoint = ".govuk-cookie-banner__content",
                        Attributes = ["text"],
                        ToProperty = "Content"
                    },
                    new AttributeMapping(){
                        MappingEntrypoint = "form",
                        Attributes = ["method", "action"],
                        ToProperty = "Form"
                    },
                    new AttributeMapping()
                    {
                        MappingEntrypoint = "form button:nth-of-type(1)",
                        Attributes = ["id", "class", "type", "value", "text", "name", "disabled"],
                        ToProperty = "AcceptCookiesButton"
                    },
                    new AttributeMapping()
                    {
                        MappingEntrypoint = "form button:nth-of-type(2)",
                        Attributes = ["id", "class", "type", "value", "text", "name", "disabled"],
                        ToProperty = "RejectCookiesButton"
                    },
                    new AttributeMapping()
                    {
                        MappingEntrypoint = "a",
                        Attributes = ["href", "text", "target", "rel"],
                        ToProperty = "ViewCookiesLink"
                    }
                ]
            }
        ];
}

