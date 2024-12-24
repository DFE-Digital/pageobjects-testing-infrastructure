using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table;
public record GDSTableComponent
{
    public TextComponent Heading { get; init; } = new TextComponent() { Text = string.Empty };
    public TableHead Head { get; init; } = new TableHead() { Rows = [] };
    public required TableBody Body { get; init; }
}
