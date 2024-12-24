namespace Dfe.Testing.Pages.Public.Components.Core.Text;
internal sealed class TextMapper : BaseDocumentSectionToComponentMapper<TextComponent>
{
    public TextMapper(IDocumentSectionFinder documentSectionFinder) : base(documentSectionFinder)
    {
    }

    public override TextComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<TextComponent>(input);
        return new()
        {
            Text = mappable.Text ?? string.Empty
        };

    }

    protected override bool IsMappableFrom(IDocumentSection part) => true; // TODO
}
