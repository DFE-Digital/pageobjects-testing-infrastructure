namespace Dfe.Testing.Pages.Components.Table;
public record TableBody : IComponent
{
    public required IEnumerable<TableRow> Rows { get; init; }
}
