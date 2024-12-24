namespace Dfe.Testing.Pages.Public.Components.GDS.Select;
internal sealed class OptionsMapper : BaseDocumentSectionToComponentMapper<OptionComponent>
{
    public OptionsMapper(IDocumentSectionFinder documentSectionFinder) : base(documentSectionFinder)
    {
    }

    public override OptionComponent Map(IDocumentSection section)
    {
        var mappable = FindMappableSection<OptionComponent>(section);

        return new()
        {
            IsSelected = mappable.HasAttribute("selected"),
            Text = mappable.Text ?? string.Empty,
            Value = mappable.GetAttribute("value") ?? string.Empty
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("option", StringComparison.OrdinalIgnoreCase);
}
