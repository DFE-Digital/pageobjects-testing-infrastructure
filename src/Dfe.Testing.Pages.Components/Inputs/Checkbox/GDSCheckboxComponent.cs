using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.Label;

namespace Dfe.Testing.Pages.Components.Inputs.Checkbox;

public sealed record GDSCheckboxComponent : IInputComponent
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required LabelComponent Label { get; init; }
    public bool Checked { get; init; } = false;
    public string Type { get; init; } = "checkbox";
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = string.Empty };
    public bool IsRequired { get; init; } = false;
}
