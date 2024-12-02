namespace Dfe.Testing.Pages.Components.Text;
public record TextComponent() : IComponent
{
    public required string Text { get; init; }
}

