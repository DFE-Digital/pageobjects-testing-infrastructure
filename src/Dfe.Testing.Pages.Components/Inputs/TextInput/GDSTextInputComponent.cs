using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.Label;

namespace Dfe.Testing.Pages.Components.Inputs.TextInput;
public record GDSTextInputComponent : IInputComponent
{
    public required LabelComponent Label { get; init; }
    public required string Name { get; init; }
    public required string Value { get; init; }
    public string Type { get; init; } = "text";
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = string.Empty };
    public bool IsNumeric { get; init; } = false;
    public string AutoComplete { get; init; } = string.Empty;
    public string? PlaceHolder { get; init; } = string.Empty;
    public string Hint { get; init; } = string.Empty;
    public bool IsRequired { get; init; } = false;
}
