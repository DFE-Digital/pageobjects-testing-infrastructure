namespace Dfe.Testing.Pages.Public.Components.Core.Inputs;
internal class CheckboxMapper : BaseDocumentSectionToComponentMapper<CheckboxComponent>
{
    public CheckboxMapper(IDocumentSectionFinder documentSectionFinder) : base(documentSectionFinder)
    {
    }

    public override CheckboxComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<CheckboxComponent>(input);
        return new CheckboxComponent()
        {
            Id = mappable.GetAttribute("id") ?? string.Empty,
            IsChecked = mappable.HasAttribute("checked"),
            IsRequired = mappable.HasAttribute("required"),
            Name = mappable.GetAttribute("name") ?? string.Empty,
            Value = mappable.GetAttribute("value") ?? string.Empty,
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section)
        => section.TagName.Equals("input", StringComparison.OrdinalIgnoreCase) &&
            section.GetAttribute("type")?.Equals("checkbox", StringComparison.OrdinalIgnoreCase) == true;
}
