using Dfe.Testing.Pages.Components.Inputs.TextInput;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class TextInputMapper : IComponentMapper<TextInputComponent>
{
    public TextInputComponent Map(IDocumentPart input)
    {
        return new()
        {
            Value = input.GetAttribute("value") ?? string.Empty,
            Name = input.GetAttribute("name") ?? string.Empty,
            Type = input.GetAttribute("type") ?? string.Empty,
            PlaceHolder = input.GetAttribute("placeholder") ?? string.Empty,
            IsRequired = input.HasAttribute("required"),
            AutoComplete = input.GetAttribute("autocomplete") ?? string.Empty
        };
    }
}
