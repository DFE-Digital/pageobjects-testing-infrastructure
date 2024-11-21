namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components.Buttons;

public record GDSButton : IComponent
{
    public required ButtonStyleType ButtonType { get; init; } = ButtonStyleType.Primary;
    public string TagName { get; init; } = "button";
    public required string? Type { get; init; }
    public required string Text { get; init; }
    public required bool Disabled { get; init; }
}

public enum ButtonStyleType
{
    Primary,
    Secondary,
    Warning
}
