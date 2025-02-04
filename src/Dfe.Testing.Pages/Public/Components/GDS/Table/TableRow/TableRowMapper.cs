using Dfe.Testing.Pages.Public.Components.GDS.Table.TableDataItem;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableHeadingItem;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;
internal sealed class TableRowMapper : IComponentMapper<TableRowComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentMapper<TableHeadingItemComponent> _tableHeadingItemMapper;
    private readonly IComponentMapper<TableDataItemComponent> _tableDataItemMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public TableRowMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentMapper<TableHeadingItemComponent> tableHeadingItemMapper,
        IComponentMapper<TableDataItemComponent> tableDataItemMapper,
        IMappingResultFactory mappingResultFactory)
    {
        _mapRequestFactory = mapRequestFactory;
        _tableHeadingItemMapper = tableHeadingItemMapper;
        _tableDataItemMapper = tableDataItemMapper;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<TableRowComponent> Map(IMapRequest<IDocumentSection> request)
    {
        IEnumerable<MappedResponse<TableHeadingItemComponent>> tableHeadingItems =
            _mapRequestFactory.CreateRequestFrom(request, nameof(TableRowComponent.Headings))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _tableHeadingItemMapper);

        IEnumerable<MappedResponse<TableDataItemComponent>> tableDataItems =
            _mapRequestFactory.CreateRequestFrom(request, nameof(TableRowComponent.DataItems))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _tableDataItemMapper);

        TableRowComponent tableRowComponent = new()
        {
            Headings = tableHeadingItems.Select(t => t.Mapped!),
            DataItems = tableDataItems.Select(t => t.Mapped!)
        };

        return _mappingResultFactory.Create(
            request.Options.MapKey,
            tableRowComponent,
            MappingStatus.Success,
            request.Document)
                .AddToMappingResults(tableHeadingItems.SelectMany(t => t.MappingResults))
                .AddToMappingResults(tableDataItems.SelectMany(t => t.MappingResults));
    }
}
