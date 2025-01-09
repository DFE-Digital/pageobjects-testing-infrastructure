using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableHeadingItem;
public record TableHeadingItemComponent
{
    public required TextComponent? Text { get; init; }
    public string Scope { get; init; } = string.Empty;
}
