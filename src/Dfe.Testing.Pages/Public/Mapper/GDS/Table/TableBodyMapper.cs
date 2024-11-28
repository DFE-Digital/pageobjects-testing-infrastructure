using Dfe.Testing.Pages.Components.Table;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS.Table;
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
