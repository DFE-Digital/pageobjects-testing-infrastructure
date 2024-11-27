using Dfe.Testing.Pages.Components.ErrorMessage;

namespace Dfe.Testing.Pages.Components.Input;
public record InputComponent : IComponent
{
    public required string Type { get; init; }
    public required string Value { get; init; }
    public required string Name { get; init; }
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = string.Empty };
    public string PlaceHolder { get; init; } = string.Empty;
    public string Hint { get; init; } = string.Empty;
    public string AutoComplete { get; init; } = string.Empty;
    public bool IsNumeric { get; init; } = false;
    public bool IsChecked { get; init; } = false;
    public bool IsRequired { get; init; } = false;
}
