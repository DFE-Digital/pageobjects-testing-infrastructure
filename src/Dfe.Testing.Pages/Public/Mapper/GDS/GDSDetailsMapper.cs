using Dfe.Testing.Pages.Components.Details;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSDetailsMapper : IComponentMapper<GDSDetailsComponent>
{
    public GDSDetailsComponent Map(IDocumentPart input)
    {
        return new()
        {
            Summary = input.FindDescendant(new CssElementSelector(".govuk-details__summary"))?.Text ?? throw new ArgumentNullException("summary on details is null"),
            Content = input.FindDescendant(new CssElementSelector(".govuk-details__text"))?.Text ?? string.Empty
        };
    }
}
