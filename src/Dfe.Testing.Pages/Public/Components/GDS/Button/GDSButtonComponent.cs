using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Button;

public record GDSButtonComponent
{
    public required ButtonStyleType ButtonType { get; init; } = ButtonStyleType.Primary;
    public required TextComponent Text { get; init; }
    public bool Disabled { get; init; } = false;
    public bool IsSubmit { get; init; } = false;
    public string Value { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}

public enum ButtonStyleType
{
    Primary,
    Secondary,
    Warning
}
