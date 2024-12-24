using Dfe.Testing.Pages.Public.Components.Core.Inputs;
using Dfe.Testing.Pages.Public.Components.Core.Label;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;

namespace Dfe.Testing.Pages.Public.Components.GDS.Radio;
public record GDSRadioComponent
{
    public required LabelComponent Label { get; init; }
    public required RadioComponent Radio { get; init; }
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = new() { Text = string.Empty } };
}
