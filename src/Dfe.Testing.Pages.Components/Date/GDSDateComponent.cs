using Dfe.Testing.Pages.Components.Fieldset;
using Dfe.Testing.Pages.Public.DocumentQueryClient;

namespace Dfe.Testing.Pages.Components.Date;
public record GDSDateComponent : IComponent
{
    public required IEnumerable<GDSFieldsetComponent> Fieldsets { get; init; }
}
