namespace Dfe.Testing.Pages.Public.Templates;
public record AnchorLinkComponent
{
    public static readonly string[] RelKnownAttributes = ["noopener", "noreferrer", "nofollow"];
    public string? Link { get; init; }
    public string? Text { get; init; }
    public bool OpensInNewTab { get; init; } = false;
    public string? Rel { get; init; }
}
