using Dfe.Testing.Pages.Components.Select;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class OptionsMapper : IComponentMapper<OptionComponent>
{
    public OptionComponent Map(IDocumentPart input)
    {
        return new()
        {
            IsSelected = input.HasAttribute("selected"),
            Text = input.Text ?? string.Empty,
            Value = input.GetAttribute("value") ?? string.Empty
        };
    }
}
