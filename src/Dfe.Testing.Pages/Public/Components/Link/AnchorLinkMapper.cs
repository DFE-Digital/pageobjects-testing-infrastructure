using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.Link;
internal sealed class AnchorLinkMapper : IComponentMapper<AnchorLinkComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IAnchorLinkComponentBuilder _anchorLinkBuilder;
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public AnchorLinkMapper(
        IAnchorLinkComponentBuilder anchrLinkBuilder,
        IComponentMapper<TextComponent> textMapper,
        IMappingResultFactory mappingResultFactory,
        IMapRequestFactory mapRequestFactory)
    {
        _anchorLinkBuilder = anchrLinkBuilder;
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<AnchorLinkComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedText =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(AnchorLinkComponent.Text)));

        _anchorLinkBuilder
            .SetLink(request.Document.GetAttribute("href") ?? string.Empty)
            .SetOpensInNewTab(request.Document.GetAttribute("target") == "_blank")
            .SetText(mappedText.Mapped!.Text);

        request.Document.GetAttribute("rel")?
            .Split(' ')
            .Select(t => t.Trim())
            .Distinct()
            .Where(t => !string.IsNullOrEmpty(t))
            .ToList().ForEach((rellAttribute) =>
            {
                _anchorLinkBuilder.AddSecurityRelAttribute(rellAttribute);
            });

        return _mappingResultFactory.Create(
            _anchorLinkBuilder.Build(),
            MappingStatus.Success,
            request.Document);
    }
}
