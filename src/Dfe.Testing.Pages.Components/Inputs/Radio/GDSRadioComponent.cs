using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.Inputs;
using Dfe.Testing.Pages.Components.Label;
namespace Dfe.Testing.Pages.Components.Inputs.Radio;
public record GDSRadioComponent : IInputComponent
{
    public required LabelComponent Label { get; init; }
    public required string Name { get; init; }
    public required string Value { get; init; }
    public string Type { get; init; } = "radio";
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = string.Empty };
    public bool IsRequired { get; init; } = false;
}
