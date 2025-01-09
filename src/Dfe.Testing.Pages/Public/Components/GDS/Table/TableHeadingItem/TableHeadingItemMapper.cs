using Dfe.Testing.Pages.Public.Components.GDS.Table.TableDataItem;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableHeadingItem;
internal class TableHeadingItemMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<TableHeadingItemComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public TableHeadingItemMapper(
        IMapRequestFactory mapRequestFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }
    public MappedResponse<TableHeadingItemComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedText = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults));

        TableHeadingItemComponent tableDataItemComponent = new()
        {
            Text = mappedText.Mapped
        };

        return _mappingResultFactory.Create(
            tableDataItemComponent,
            MappingStatus.Success,
            request.From);
    }
}
