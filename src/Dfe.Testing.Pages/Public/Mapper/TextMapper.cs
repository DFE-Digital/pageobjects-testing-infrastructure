using Dfe.Testing.Pages.Components;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class TextMapper : IComponentMapper<TextComponent>
{
    public TextComponent Map(IDocumentPart input)
        => new()
        {
            Text = input.Text ?? string.Empty
        };
}
