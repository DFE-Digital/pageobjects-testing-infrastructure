namespace Dfe.Testing.Pages.Public.Components.Text;
internal sealed class TextMapper : IComponentMapper<TextComponent>
{
    public TextComponent Map(IDocumentPart input)
        => new()
        {
            Text = input.Text ?? string.Empty
        };
}
