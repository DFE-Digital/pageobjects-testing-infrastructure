namespace Dfe.Testing.Pages.Components;
public record TextComponent() : IComponent
{
    public required string Text { get; init; }
}

