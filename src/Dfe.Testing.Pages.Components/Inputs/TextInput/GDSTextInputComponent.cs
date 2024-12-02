using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.Label;

namespace Dfe.Testing.Pages.Components.Inputs.TextInput;
public record GDSTextInputComponent : IComponent
{
    public required LabelComponent Label { get; init; }
    public required TextInputComponent Input { get; init; }
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = string.Empty };
}
