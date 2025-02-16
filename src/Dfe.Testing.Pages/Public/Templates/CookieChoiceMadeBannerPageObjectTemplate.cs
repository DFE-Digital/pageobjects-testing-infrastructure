using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.Testing.Pages.Public.Templates;
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
            Mapping = [
                new AttributeMapping()
                {
                    MappingEntrypoint = "form p",
                    Attributes = ["text"],
                    ToProperty = "Content"
                },
                new AttributeMapping()
                {
                    MappingEntrypoint = "form a",
                    Attributes = ["href", "text", "rel", "id", "target"],
                    ToProperty = "ChangeCookiesLink"
                },
                new AttributeMapping(){
                    MappingEntrypoint = "form",
                    Attributes = ["method", "action"],
                    ToProperty = "Form"
                },
                new AttributeMapping(){
                    MappingEntrypoint = "form button",
                    Attributes = ["id", "type", "text", "class", "name"],
                    ToProperty = "HideCookiesButton"
                },
            ],
            Children = []
        }
    ];
}
