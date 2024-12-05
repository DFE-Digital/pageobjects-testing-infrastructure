namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
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
