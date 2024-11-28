using Dfe.Testing.Pages.Components.Table;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS.Table;
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
