using Dfe.Testing.Pages.Components.Table;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS.Table;
internal class TableDataItemMapper : IComponentMapper<TableDataItem>
{
    public TableDataItem Map(IDocumentPart input)
    {
        return new()
        {
            Text = input.Text
        };
    }
}
