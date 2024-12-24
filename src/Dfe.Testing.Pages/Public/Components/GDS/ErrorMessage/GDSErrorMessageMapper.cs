using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
internal sealed class GDSErrorMessageMapper : BaseDocumentSectionToComponentMapper<GDSErrorMessageComponent>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;

    public GDSErrorMessageMapper(IDocumentSectionFinder documentSectionFinder, IMapper<IDocumentSection, TextComponent> textMapper) : base(documentSectionFinder)
    {
        _textMapper = textMapper;
    }

    public override GDSErrorMessageComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<GDSErrorMessageComponent>(input);
        return new()
        {
            ErrorMessage =
                _documentSectionFinder.FindMany(mappable, new CssElementSelector(".govuk-error-message"))
                    .FirstOrDefault()?
                    .MapWith(_textMapper) ?? new TextComponent()
                    {
                        Text = string.Empty
                    }
        };
    }

    protected override bool IsMappableFrom(IDocumentSection part) => true; //TODO something to do with contains an error message
}
