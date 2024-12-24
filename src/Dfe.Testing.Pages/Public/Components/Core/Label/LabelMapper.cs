using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.Core.Label;
internal sealed class LabelMapper : BaseDocumentSectionToComponentMapper<LabelComponent>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;

    public LabelMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TextComponent> textMapper) : base(documentSectionFinder)
    {
        _textMapper = textMapper;
    }
    public override LabelComponent Map(IDocumentSection section)
    {
        var mappable = FindMappableSection<LabelComponent>(section);
        return new()
        {
            For = mappable?.GetAttribute("for") ?? string.Empty,
            Text = _textMapper.Map(mappable!)
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("label", StringComparison.OrdinalIgnoreCase);
}
