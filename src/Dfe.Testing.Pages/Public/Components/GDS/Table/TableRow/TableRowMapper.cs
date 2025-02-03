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
        IEnumerable<TableHeadingItemComponent> tableHeadingItems =
            _mapRequestFactory.CreateRequestFrom(request, nameof(TableRowComponent.Headings))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _tableHeadingItemMapper)
                .AddToMappingResults(request.MappedResults)
                .Select(t => t.Mapped!);

        IEnumerable<TableDataItemComponent> tableDataItems =
            _mapRequestFactory.CreateRequestFrom(request, nameof(TableRowComponent.DataItems))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _tableDataItemMapper)
                .AddToMappingResults(request.MappedResults)
                .Select(t => t.Mapped!);

        TableRowComponent tableRowComponent = new()
        {
            Headings = tableHeadingItems,
            DataItems = tableDataItems
        };

        return _mappingResultFactory.Create(
            tableRowComponent,
            MappingStatus.Success,
            request.Document);
    }
}
