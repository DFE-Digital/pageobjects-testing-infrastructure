namespace Dfe.Testing.Pages.Components.Table;
public record TableHead : IComponent
{
    public required IEnumerable<TableRow> Rows { get; init; }
}
