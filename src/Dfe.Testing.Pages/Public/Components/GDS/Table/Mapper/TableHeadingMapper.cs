namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
internal class TableHeadingMapper : IComponentMapper<TableHeadingItem>
{
    public TableHeadingItem Map(IDocumentPart input)
    {
        return new()
        {
            Scope = input.GetAttribute("scope") ?? string.Empty,
            Text = input.Text ?? string.Empty
        };
    }
}
