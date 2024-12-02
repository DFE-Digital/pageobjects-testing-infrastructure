using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.Label;
namespace Dfe.Testing.Pages.Components.Inputs.Radio;
public record GDSRadioComponent : IComponent
{
    public required LabelComponent Label { get; init; }
    public required RadioComponent Radio { get; init; }
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = string.Empty };
}
