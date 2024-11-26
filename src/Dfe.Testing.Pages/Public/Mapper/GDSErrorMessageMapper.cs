using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class GDSErrorMessageMapper : IComponentMapper<GDSErrorMessageComponent>
{
    public GDSErrorMessageComponent Map(IDocumentPart input)
    {
        return new()
        {
            ErrorMessage = input.GetChild(new CssSelector(".govuk-error-message"))?.Text ?? string.Empty
        };
    }
}
