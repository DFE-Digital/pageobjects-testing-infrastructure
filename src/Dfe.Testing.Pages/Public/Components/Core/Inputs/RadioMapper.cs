namespace Dfe.Testing.Pages.Public.Components.Core.Inputs;
internal sealed class RadioMapper : BaseDocumentSectionToComponentMapper<RadioComponent>
{
    public RadioMapper(IDocumentSectionFinder documentSectionFinder) : base(documentSectionFinder)
    {
    }

    public override RadioComponent Map(IDocumentSection section)
    {
        var mappable = FindMappableSection<RadioComponent>(section);
        return new()
        {
            Id = mappable.GetAttribute("id") ?? string.Empty,
            Value = mappable.GetAttribute("value") ?? string.Empty,
            Name = mappable.GetAttribute("name") ?? string.Empty,
            IsRequired = mappable.HasAttribute("required")
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section)
        => section.TagName.Equals("input", StringComparison.OrdinalIgnoreCase) &&
            (section.GetAttribute("type")?.Equals("radio", StringComparison.OrdinalIgnoreCase) ?? false);
}
