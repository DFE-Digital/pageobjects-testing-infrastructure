namespace Dfe.Testing.Pages.Components;
public record AnchorLinkComponent : IComponent
{
    public required string LinkedTo { get; init; }
    public required string Text { get; init; }
    public bool OpensInNewTab { get; init; } = false;
}
