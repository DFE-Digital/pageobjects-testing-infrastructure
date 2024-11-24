namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

public record GDSButton : IComponent
{
    public required ButtonStyleType ButtonType { get; init; } = ButtonStyleType.Primary;
    public string TagName { get; init; } = "button";
    public bool IsSubmit { get; init; } = false;
    public required string Text { get; init; }
    public required bool Disabled { get; init; }
}

public enum ButtonStyleType
{
    Primary,
    Secondary,
    Warning
}
