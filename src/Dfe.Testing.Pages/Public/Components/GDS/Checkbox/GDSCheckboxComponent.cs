using Dfe.Testing.Pages.Public.Components.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;

namespace Dfe.Testing.Pages.Public.Components.GDS.Checkbox;

public sealed record GDSCheckboxComponent
{
    internal GDSCheckboxComponent() { }
    public required CheckboxComponent? Checkbox { get; init; }
    public required LabelComponent? Label { get; init; }
    public required GDSErrorMessageComponent? Error { get; init; }
    public bool IsRequired { get; init; } = false;
}

public interface IGDSCheckboxBuilder
{
    public IGDSCheckboxBuilder SetCheckbox(CheckboxComponent checkbox);
    public IGDSCheckboxBuilder SetCheckbox(Action<ICheckboxBuilder> configure);
    public IGDSCheckboxBuilder SetErrorMessage(string error);
    public IGDSCheckboxBuilder SetLabelText(string text);
    public IGDSCheckboxBuilder SetLabelFor(string text);
    GDSCheckboxComponent Build();
}
