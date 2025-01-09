using System.Collections.Generic;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableHeadingItem;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableHead;
internal class TableHeadMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableHeadComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableRowComponent>> _rowMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public TableHeadMapper(
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

    public MappedResponse<TableHeadComponent> Map(IMapRequest<IDocumentSection> request)
    {
        IEnumerable<MappedResponse<TableRowComponent>> mappedRows = request.FindManyDescendantsAndMap(
            _mapRequestFactory,
            _componentSelectorFactory.GetSelector<TableRowComponent>(),
            _rowMapper)
        .AddMappedResponseToResults(request.MappingResults);

        TableHeadComponent tableHeadComponent = new()
        {
            Rows = mappedRows.Select(t => t.Mapped!)
        };

        return _mappingResultFactory.Create(
            tableHeadComponent,
            MappingStatus.Success,
            request.From);
    }
}
