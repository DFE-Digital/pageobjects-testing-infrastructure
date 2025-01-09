using Dfe.Testing.Pages.Public.Components.GDS.Table.TableDataItem;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableHeadingItem;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;
internal sealed class TableRowMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableRowComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableHeadingItemComponent>> _tableHeadingItemMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableDataItemComponent>> _tableDataItemMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public TableRowMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableHeadingItemComponent>> tableHeadingItemMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableDataItemComponent>> tableDataItemMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(componentSelectorFactory);
        ArgumentNullException.ThrowIfNull(tableHeadingItemMapper);
        ArgumentNullException.ThrowIfNull(tableDataItemMapper);
        _mapRequestFactory = mapRequestFactory;
        _componentSelectorFactory = componentSelectorFactory;
        _tableHeadingItemMapper = tableHeadingItemMapper;
        _tableDataItemMapper = tableDataItemMapper;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<TableRowComponent> Map(IMapRequest<IDocumentSection> request)
    {
        IEnumerable<TableHeadingItemComponent> tableHeadingItems = request.FindManyDescendantsAndMap(
            _mapRequestFactory,
            _componentSelectorFactory.GetSelector<TableHeadingItemComponent>(),
            _tableHeadingItemMapper).Select(t => t.Mapped!);

        IEnumerable<TableDataItemComponent> tableDataItems = request.FindManyDescendantsAndMap(
            _mapRequestFactory,
            _componentSelectorFactory.GetSelector<TableDataItemComponent>(),
            _tableDataItemMapper).Select(t => t.Mapped!);

        TableRowComponent tableRowComponent = new()
        {
            Headings = tableHeadingItems,
            DataItem = tableDataItems
        };

        return _mappingResultFactory.Create(
            tableRowComponent,
            MappingStatus.Success,
            request.From);
    }
}
