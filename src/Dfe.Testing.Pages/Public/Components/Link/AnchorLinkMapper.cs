using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.Link;
internal sealed class AnchorLinkMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IAnchorLinkComponentBuilder _anchorLinkComponentBuilder;

    public AnchorLinkMapper(
        IMapRequestFactory mapRequestFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper,
        IMappingResultFactory mappingResultFactory,
        IAnchorLinkComponentBuilder anchorLinkComponentBuilder)
    {
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
        _anchorLinkComponentBuilder = anchorLinkComponentBuilder;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<AnchorLinkComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedText = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults))
            .AddMappedResponseToResults(request.MappingResults);


        IAnchorLinkComponentBuilder builder =
            _anchorLinkComponentBuilder
                .SetLink(request.From.GetAttribute("href") ?? string.Empty)
                .SetOpensInNewTab(request.From.GetAttribute("target") == "_blank")
                .SetText(mappedText.Mapped!.Text);

        request.From.GetAttribute("rel")?
            .Split(' ')
            .Select(t => t.Trim())
            .Where(t => !string.IsNullOrEmpty(t))
            .ToList()
            .ForEach(attribute =>
            {
                builder.AddRelAttribute(attribute);
            });

        return _mappingResultFactory.Create(
            builder.Build(),
            MappingStatus.Success,
            request.From);
    }
}
