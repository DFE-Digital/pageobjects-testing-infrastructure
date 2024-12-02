using Dfe.Testing.Pages.Components.Inputs.TextInput;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
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
