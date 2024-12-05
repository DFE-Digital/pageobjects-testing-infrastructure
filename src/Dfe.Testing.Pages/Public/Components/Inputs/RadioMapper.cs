namespace Dfe.Testing.Pages.Public.Components.Inputs;
internal sealed class RadioMapper : IComponentMapper<RadioComponent>
{
    public RadioComponent Map(IDocumentPart input)
    {
        return new()
        {
            Id = input.GetAttribute("id") ?? string.Empty,
            Value = input.GetAttribute("value") ?? string.Empty,
            Name = input.GetAttribute("name") ?? string.Empty,
            IsRequired = input.HasAttribute("required")
        };
    }
}
