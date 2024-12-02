using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSErrorMessageMapper : IComponentMapper<GDSErrorMessageComponent>
{
    public GDSErrorMessageComponent Map(IDocumentPart input)
    {
        return new()
        {
            ErrorMessage = input.GetChild(new CssElementSelector(".govuk-error-message"))?.Text ?? string.Empty
        };
    }
}
