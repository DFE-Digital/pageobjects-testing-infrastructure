using Dfe.Testing.Pages.Components.Table;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS.Table;
internal sealed class TableRowMapper : IComponentMapper<TableRow>
{
    private readonly ComponentFactory<TableHeadingItem> _tableHeadingFactory;
    private readonly ComponentFactory<TableDataItem> _tableDataItemFactory;

    public TableRowMapper(
        ComponentFactory<TableHeadingItem> tableHeadingFactory,
        ComponentFactory<TableDataItem> tableDataItemFactory)
    {
        ArgumentNullException.ThrowIfNull(tableHeadingFactory);
        ArgumentNullException.ThrowIfNull(tableDataItemFactory);
        _tableHeadingFactory = tableHeadingFactory;
        _tableDataItemFactory = tableDataItemFactory;
    }
    public TableRow Map(IDocumentPart input)
    {
        return new TableRow()
        {
            Headings = _tableHeadingFactory.GetManyFromPart(input),
            DataItem = _tableDataItemFactory.GetManyFromPart(input)
        };
    }
}
