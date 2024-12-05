namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
internal class TableHeadMapper : IComponentMapper<TableHead>
{
    private readonly ComponentFactory<TableRow> _rowFactory;

    public TableHeadMapper(ComponentFactory<TableRow> rowFactory)
    {
        ArgumentNullException.ThrowIfNull(rowFactory);
        _rowFactory = rowFactory;
    }

    public TableHead Map(IDocumentPart input)
    {
        return new()
        {
            Rows = _rowFactory.GetManyFromPart(input)
        };
    }
}
