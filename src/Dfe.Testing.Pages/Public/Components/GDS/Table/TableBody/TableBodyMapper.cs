using Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableBody;
internal class TableBodyMapper : IComponentMapper<TableBodyComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<TableRowComponent> _rowMapper;

    public TableBodyMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentMapper<TableRowComponent> rowMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(rowMapper);
        _rowMapper = rowMapper;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<TableBodyComponent> Map(IMapRequest<IDocumentSection> request)
    {
        IEnumerable<MappedResponse<TableRowComponent>> mappedRows =
            _mapRequestFactory.CreateRequestFrom(request, nameof(TableBodyComponent.Rows))
            .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _rowMapper)
            .AddToMappingResults(request.MappedResults);

        TableBodyComponent tableBody = new()
        {
            Rows = mappedRows.Select(t => t.Mapped!)
        };

        return _mappingResultFactory.Create(
            tableBody,
            MappingStatus.Success,
            request.Document);
    }
}
