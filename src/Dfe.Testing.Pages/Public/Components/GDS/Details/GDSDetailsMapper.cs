using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Details;
internal sealed class GDSDetailsMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSDetailsComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;

    public GDSDetailsMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper)
    {
        _mappingResultFactory = mappingResultFactory;
        _textMapper = textMapper;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSDetailsComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> summary = _textMapper.Map(
            _mapRequestFactory.Create(
            request.From,
            request.MappingResults,
            new CssElementSelector(".govuk-details__summary")));

        request.MappingResults.Add(summary.MappingResult);


        MappedResponse<TextComponent> content = _textMapper.Map(_mapRequestFactory.Create(
            request.From,
            request.MappingResults,
            new CssElementSelector(".govuk-details__text")));

        request.MappingResults.Add(content.MappingResult);

        GDSDetailsComponent component = new()
        {
            Summary = summary.Mapped,
            Content = content.Mapped
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.From);
    }
}
