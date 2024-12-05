namespace Dfe.Testing.Pages.Public.Components.Text;
public record TextComponent() : IComponent
{
    public required string Text { get; init; }
}

