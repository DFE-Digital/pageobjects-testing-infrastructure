using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableHeadingItem;
internal class TableHeadingItemMapper : IComponentMapper<TableHeadingItemComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public TableHeadingItemMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentMapper<TextComponent> textMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        _mapRequestFactory = mapRequestFactory;
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<TableHeadingItemComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedText =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(TableHeadingItemComponent.Text)))
            .AddToMappingResults(request.MappedResults);

        TableHeadingItemComponent tableDataItemComponent = new()
        {
            Text = mappedText.Mapped
        };

        return _mappingResultFactory.Create(
            request.Options.MapKey,
            tableDataItemComponent,
            MappingStatus.Success,
            request.Document);
    }
}
