using Dfe.Testing.Pages.Public.Components.GDS.Table.GDSTable;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableBody;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableHead;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
internal sealed class GDSTableMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSTableComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableHeadComponent>> _tableHeadMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableBodyComponent>> _tableBodyMapper;

    public GDSTableMapper(
        IMapRequestFactory mapRequestFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableHeadComponent>> tableHeadMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableBodyComponent>> tableBodyMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        ArgumentNullException.ThrowIfNull(tableHeadMapper);
        ArgumentNullException.ThrowIfNull(tableBodyMapper);
        _mapRequestFactory = mapRequestFactory;
        _textMapper = textMapper;
        _tableHeadMapper = tableHeadMapper;
        _tableBodyMapper = tableBodyMapper;
        _mappingResultFactory = mappingResultFactory;
    }
    public MappedResponse<GDSTableComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedHeading = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector("caption")))
            .AddMappedResponseToResults(request.MappingResults);

        MappedResponse<TableHeadComponent> mappedTableHead = _tableHeadMapper.Map(_mapRequestFactory.Create(
                request.From,
                request.MappingResults))
            .AddMappedResponseToResults(request.MappingResults);

        MappedResponse<TableBodyComponent> mappedTableBody = _tableBodyMapper.Map(_mapRequestFactory.Create(
                request.From,
                request.MappingResults))
            .AddMappedResponseToResults(request.MappingResults);

        GDSTableComponent tableBody = new()
        {
            Heading = mappedHeading.Mapped,
            Head = mappedTableHead.Mapped,
            Body = mappedTableBody.Mapped
        };

        return _mappingResultFactory.Create(
            tableBody,
            MappingStatus.Success,
            request.From);
    }
}
