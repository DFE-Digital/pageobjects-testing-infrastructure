using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.Link;
internal sealed class AnchorLinkMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public AnchorLinkMapper(
        IMapRequestFactory mapRequestFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper,
        IMappingResultFactory mappingResultFactory)
    {
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<AnchorLinkComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedText = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults))
            .AddMappedResponseToResults(request.MappingResults);

        IAnchorLinkComponentBuilder linkBuilder = new AnchorLinkComponentBuilder()
            .SetLink(request.From.GetAttribute("href") ?? string.Empty)
            .SetOpensInNewTab(request.From.GetAttribute("target") == "_blank")
            .SetText(mappedText.Mapped!.Text);

        IEnumerable<string> relAttributes = request.From.GetAttribute("rel")?
            .Split(' ')
            .Select(t => t.Trim())
            .Distinct()
            .Where(t => !string.IsNullOrEmpty(t)) ?? [];

        relAttributes.ToList().ForEach(attribute => linkBuilder.AddSecurityRelAttribute(attribute));

        return _mappingResultFactory.Create(
            linkBuilder.Build(),
            MappingStatus.Success,
            request.From);
    }
}
