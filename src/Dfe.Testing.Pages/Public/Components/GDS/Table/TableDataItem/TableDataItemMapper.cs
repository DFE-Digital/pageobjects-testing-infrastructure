using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableDataItem;
internal class TableDataItemMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableDataItemComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public TableDataItemMapper(
        IMapRequestFactory mapRequestFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        _mapRequestFactory = mapRequestFactory;
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<TableDataItemComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedText = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults))
        .AddMappedResponseToResults(request.MappingResults);

        TableDataItemComponent tableDataItemComponent = new()
        {
            Text = mappedText.Mapped
        };

        return _mappingResultFactory.Create(
            tableDataItemComponent,
            MappingStatus.Success,
            request.From);
    }
}
