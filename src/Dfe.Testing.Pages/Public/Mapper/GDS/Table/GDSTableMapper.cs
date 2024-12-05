using Dfe.Testing.Pages.Components.Table;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS.Table;
internal sealed class GDSTableMapper : IComponentMapper<GDSTableComponent>
{
    private readonly ComponentFactory<TableHead> _tableHeadFactory;
    private readonly ComponentFactory<TableBody> _tableBodyFactory;


    public GDSTableMapper(
        ComponentFactory<TableHead> tableHeadFactory,
        ComponentFactory<TableBody> tableBodyFactory)
    {
        ArgumentNullException.ThrowIfNull(tableHeadFactory);
        ArgumentNullException.ThrowIfNull(tableBodyFactory);
        _tableHeadFactory = tableHeadFactory;
        _tableBodyFactory = tableBodyFactory;
    }
    public GDSTableComponent Map(IDocumentPart input)
    {
        return new()
        {
            // TODO should this be a component so it can be overwritten for headings?
            Heading = input.FindDescendant(new CssElementSelector("caption"))?.Text ?? string.Empty,
            Head = _tableHeadFactory.GetManyFromPart(input).Single(),
            Body = _tableBodyFactory.GetManyFromPart(input).Single()
        };
    }
}
