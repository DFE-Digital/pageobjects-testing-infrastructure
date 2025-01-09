using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;
using Dfe.Testing.Pages.Public.Components.Radio;

namespace Dfe.Testing.Pages.Public.Components.GDS.Radio;
public record GDSRadioComponent
{
    public required LabelComponent? Label { get; init; }
    public required RadioComponent? Radio { get; init; }
    public required GDSErrorMessageComponent? Error { get; init; }
}
