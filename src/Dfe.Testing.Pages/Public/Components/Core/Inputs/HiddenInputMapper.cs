namespace Dfe.Testing.Pages.Public.Components.Core.Inputs;
internal sealed class HiddenInputMapper : BaseDocumentSectionToComponentMapper<HiddenInputComponent>
{
    public HiddenInputMapper(IDocumentSectionFinder documentSectionFinder) : base(documentSectionFinder)
    {
    }

    public override HiddenInputComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<HiddenInputComponent>(input);
        return new()
        {
            Name = mappable.GetAttribute("name") ?? string.Empty,
            Value = mappable.GetAttribute("value") ?? string.Empty
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section)
        => section.TagName.Equals("input", StringComparison.OrdinalIgnoreCase) &&
            section.GetAttribute("type")?.Equals("hidden", StringComparison.OrdinalIgnoreCase) == true;
}
