namespace Dfe.Testing.Pages.Components.Table;
public record GDSTableComponent : IComponent
{
    public required string Heading { get; init; }
    public TableHead Head { get; init; } = new TableHead() { Rows = [] };
    public required TableBody Body { get; init; }
}

public record TableHead : IComponent
{
    public required IEnumerable<TableRow> Rows { get; init; }
}
public record TableBody : IComponent
{
    public required IEnumerable<TableRow> Rows { get; init; }
}

public record TableRow : IComponent
{
    public IEnumerable<TableHeading> Headings { get; init; } = [];
    public IEnumerable<TableDataItem> DataItem { get; init; } = [];

}

public record TableHeading : IComponent
{
    public required string Text { get; init; }
    public string Scope { get; init; } = string.Empty;
}

public record TableDataItem : IComponent
{
    public required string Text { get; init; }
}
