using Dfe.Testing.Pages.Public.Components.GDS.Table.TableBody;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableHead;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.GDSTable;
internal sealed class GDSTableMapper : IComponentMapper<GDSTableComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IComponentMapper<TableHeadComponent> _tableHeadMapper;
    private readonly IComponentMapper<TableBodyComponent> _tableBodyMapper;

    public GDSTableMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentMapper<TextComponent> textMapper,
        IComponentMapper<TableHeadComponent> tableHeadMapper,
        IComponentMapper<TableBodyComponent> tableBodyMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        ArgumentNullException.ThrowIfNull(tableHeadMapper);
        ArgumentNullException.ThrowIfNull(tableBodyMapper);
        _textMapper = textMapper;
        _tableHeadMapper = tableHeadMapper;
        _tableBodyMapper = tableBodyMapper;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSTableComponent> Map(IMapRequest<IDocumentSection> request)
    {
        var mappedHeading = _textMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSTableComponent.Heading)))
                .AddToMappingResults(request.MappedResults);

        var mappedTableHead = _tableHeadMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSTableComponent.Head)))
                .AddToMappingResults(request.MappedResults);

        var mappedTableBody = _tableBodyMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSTableComponent.Body)))
                .AddToMappingResults(request.MappedResults);

        GDSTableComponent tableBody = new()
        {
            Heading = mappedHeading.Mapped,
            Head = mappedTableHead.Mapped,
            Body = mappedTableBody.Mapped
        };

        return _mappingResultFactory.Create(
            tableBody,
            MappingStatus.Success,
            request.Document);
    }
}
