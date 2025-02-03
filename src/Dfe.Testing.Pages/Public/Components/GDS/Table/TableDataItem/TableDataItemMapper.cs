using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableDataItem;
internal class TableDataItemMapper : IComponentMapper<TableDataItemComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public TableDataItemMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentMapper<TextComponent> textMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<TableDataItemComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedText =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(TableDataItemComponent.Text)))
            .AddToMappingResults(request.MappedResults);

        TableDataItemComponent tableDataItemComponent = new()
        {
            Text = mappedText.Mapped
        };

        return _mappingResultFactory.Create(
            tableDataItemComponent,
            MappingStatus.Success,
            request.Document);
    }
}
