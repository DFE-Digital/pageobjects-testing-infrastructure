namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components;

public record GDSButtonComponent : IComponent
{
    public required ButtonStyleType ButtonType { get; init; } = ButtonStyleType.Primary;
    public required string Text { get; init; } = string.Empty;
    public bool Disabled { get; init; } = false;
    public bool IsSubmit { get; init; } = false;
    public string TagName { get; init; } = "button";
    public string Value { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}

public enum ButtonStyleType
{
    Primary,
    Secondary,
    Warning
}
