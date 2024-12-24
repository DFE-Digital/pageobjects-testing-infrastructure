using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Parts;
public record TableHeadingItem
{
    public required TextComponent Text { get; init; } = new() { Text = string.Empty };
    public string Scope { get; init; } = string.Empty;
}
