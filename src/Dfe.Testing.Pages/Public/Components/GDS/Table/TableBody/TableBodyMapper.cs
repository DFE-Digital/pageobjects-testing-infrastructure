using Dfe.Testing.Pages.Public.Components.GDS.Table.GDSTable;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableBody;
internal class TableBodyMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableBodyComponent>>
{
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableRowComponent>> _rowMapper;

    public TableBodyMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableRowComponent>> rowMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(rowMapper);
        _mapRequestFactory = mapRequestFactory;
        _componentSelectorFactory = componentSelectorFactory;
        _rowMapper = rowMapper;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<TableBodyComponent> Map(IMapRequest<IDocumentSection> request)
    {
        IEnumerable<MappedResponse<TableRowComponent>> mappedRows = request.FindManyDescendantsAndMap(
            _mapRequestFactory,
            _componentSelectorFactory.GetSelector<TableRowComponent>(),
            _rowMapper);

        TableBodyComponent tableBody = new()
        {
            Rows = mappedRows.Select(t => t.Mapped!)
        };

        return _mappingResultFactory.Create(
            tableBody,
            MappingStatus.Success,
            request.From);
    }
}
