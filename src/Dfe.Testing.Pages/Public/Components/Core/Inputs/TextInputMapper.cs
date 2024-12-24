namespace Dfe.Testing.Pages.Public.Components.Core.Inputs;
internal sealed class TextInputMapper : BaseDocumentSectionToComponentMapper<TextInputComponent>
{
    public TextInputMapper(IDocumentSectionFinder documentSectionFinder) : base(documentSectionFinder)
    {
    }

    public override TextInputComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<TextInputComponent>(input);
        return new()
        {
            Value = mappable.GetAttribute("value") ?? string.Empty,
            Name = mappable.GetAttribute("name") ?? string.Empty,
            Type = mappable.GetAttribute("type") ?? string.Empty,
            PlaceHolder = mappable.GetAttribute("placeholder") ?? string.Empty,
            IsRequired = mappable.HasAttribute("required"),
            AutoComplete = mappable.GetAttribute("autocomplete") ?? string.Empty
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName == "input" && section.GetAttribute("type") == "text";

}
