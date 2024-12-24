using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.Core.Link;
public sealed class AnchorLinkMapper : BaseDocumentSectionToComponentMapper<AnchorLinkComponent>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;
    public AnchorLinkMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TextComponent> textMapper) : base(documentSectionFinder)
    {
        _textMapper = textMapper;
    }

    public override AnchorLinkComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<AnchorLinkComponent>(input);
        return new()
        {
            LinkedTo = mappable.GetAttribute("href")! ?? string.Empty,
            Text = _textMapper.Map(mappable),
            OpensInNewTab = mappable.GetAttribute("target") == "_blank"
        };
    }

    protected override bool IsMappableFrom(IDocumentSection part) => part.TagName == "a";
}
