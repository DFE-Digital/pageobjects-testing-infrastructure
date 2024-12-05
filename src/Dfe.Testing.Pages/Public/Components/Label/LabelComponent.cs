namespace Dfe.Testing.Pages.Public.Components.Label;
public record LabelComponent : IComponent
{
    public required string For { get; init; }
    public required string Text { get; init; }
}
