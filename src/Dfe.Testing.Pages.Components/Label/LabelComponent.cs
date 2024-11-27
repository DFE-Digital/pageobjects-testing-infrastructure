namespace Dfe.Testing.Pages.Components.Label;
public record LabelComponent : IComponent
{
    public required string For { get; init; }
    public required string Text { get; init; }
}
