using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Details;
public record GDSDetailsComponent
{
    public required TextComponent Summary { get; init; }
    public required TextComponent Content { get; init; }
}
