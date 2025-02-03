using Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableHead;
internal class TableHeadMapper : IComponentMapper<TableHeadComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentMapper<TableRowComponent> _rowMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public TableHeadMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentMapper<TableRowComponent> rowMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(rowMapper);
        _mapRequestFactory = mapRequestFactory;
        _rowMapper = rowMapper;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<TableHeadComponent> Map(IMapRequest<IDocumentSection> request)
    {
        IEnumerable<MappedResponse<TableRowComponent>> mappedRows =
            _mapRequestFactory.CreateRequestFrom(request, nameof(TableHeadComponent.Rows))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _rowMapper)
                .AddToMappingResults(request.MappedResults);

        TableHeadComponent tableHeadComponent = new()
        {
            Rows = mappedRows.Select(t => t.Mapped!)
        };

        return _mappingResultFactory.Create(
            tableHeadComponent,
            MappingStatus.Success,
            request.Document);
    }
}
