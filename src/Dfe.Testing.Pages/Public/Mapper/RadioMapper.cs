using Dfe.Testing.Pages.Components.Inputs.Radio;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
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
