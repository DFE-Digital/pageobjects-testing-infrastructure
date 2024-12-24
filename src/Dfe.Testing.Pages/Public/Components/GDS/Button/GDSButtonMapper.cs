using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Button;
internal class GDSButtonMapper : BaseDocumentSectionToComponentMapper<GDSButtonComponent>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;

    public GDSButtonMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TextComponent> textMapper) : base(documentSectionFinder)
    {
        _textMapper = textMapper;
    }

    internal static IElementSelector SecondaryButtonStyle => new CssElementSelector("govuk-button--secondary");
    internal static IElementSelector WarningButtonStyle => new CssElementSelector(".govuk-button--warning");

    public override GDSButtonComponent Map(IDocumentSection part)
    {
        IDocumentSection mappable = FindMappableSection<GDSButtonComponent>(part);
        var buttonStyles = mappable.GetAttribute("class") ?? string.Empty;

        return new()
        {
            ButtonType =
                buttonStyles.Contains(SecondaryButtonStyle.ToSelector()) ? ButtonStyleType.Secondary :
                buttonStyles.Contains(WarningButtonStyle.ToSelector()) ? ButtonStyleType.Warning
                    : ButtonStyleType.Primary,
            Text = _textMapper.Map(mappable),
            Disabled = mappable.HasAttribute("disabled"),
            IsSubmit = mappable.GetAttribute("type") == "submit",
            Name = mappable.GetAttribute("name") ?? string.Empty,
            Value = mappable.GetAttribute("value") ?? string.Empty
        };
    }

    protected override bool IsMappableFrom(IDocumentSection part) => part.TagName.Equals("button", StringComparison.OrdinalIgnoreCase);
}
