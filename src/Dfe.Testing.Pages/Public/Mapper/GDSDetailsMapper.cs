using Dfe.Testing.Pages.Components.Details;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class GDSDetailsMapper : IComponentMapper<GDSDetailsComponent>
{
    public GDSDetailsComponent Map(IDocumentPart input)
    {
        return new()
        {
            Summary = input.GetChild(new CssSelector(".govuk-details__summary"))?.Text ?? throw new ArgumentNullException("summary on details is null"),
            Content = input.GetChild(new CssSelector(".govuk-details__text"))?.Text ?? string.Empty
        };
    }
}
