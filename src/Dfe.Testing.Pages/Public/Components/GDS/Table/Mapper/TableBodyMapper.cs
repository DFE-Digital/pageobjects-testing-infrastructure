namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
internal class TableBodyMapper : IComponentMapper<TableBody>
{
    private readonly ComponentFactory<TableRow> _rowFactory;

    public TableBodyMapper(ComponentFactory<TableRow> rowFactory)
    {
        ArgumentNullException.ThrowIfNull(rowFactory);
        _rowFactory = rowFactory;
    }

    public TableBody Map(IDocumentPart input)
    {
        return new()
        {
            Rows = _rowFactory.GetManyFromPart(input)
        };
    }
}
