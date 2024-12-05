namespace Dfe.Testing.Pages.Public.Components.Inputs;
internal sealed class HiddenInputMapper : IComponentMapper<HiddenInputComponent>
{
    public HiddenInputComponent Map(IDocumentPart input)
    {
        return new()
        {
            Name = input.GetAttribute("name") ?? string.Empty,
            Value = input.GetAttribute("value") ?? string.Empty
        };
    }
}
