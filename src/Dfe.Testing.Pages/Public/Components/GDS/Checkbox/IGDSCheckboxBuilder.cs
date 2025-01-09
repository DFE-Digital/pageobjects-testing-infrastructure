using Dfe.Testing.Pages.Public.Components.Checkbox;

namespace Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
public interface IGDSCheckboxBuilder
{
    public IGDSCheckboxBuilder SetCheckbox(CheckboxComponent checkbox);
    public IGDSCheckboxBuilder SetCheckbox(Action<ICheckboxBuilder> configure);
    public IGDSCheckboxBuilder SetErrorMessage(string error);
    public IGDSCheckboxBuilder SetLabelText(string text);
    public IGDSCheckboxBuilder SetLabelFor(string text);
    GDSCheckboxComponent Build();
}

internal sealed class GDSCheckboxBuilder : IGDSCheckboxBuilder
{
    private readonly ICheckboxBuilder _checkboxBuilder;
    private string _labelText = string.Empty;
    private string _labelFor = string.Empty;
    private string _errorMessage = string.Empty;

    public GDSCheckboxBuilder(ICheckboxBuilder checkboxBuilder)
    {
        _checkboxBuilder = checkboxBuilder;
    }

    public GDSCheckboxComponent Build() => new()
    {
        Checkbox = _checkboxBuilder.Build(),
        Label = new()
        {
            Text = new()
            {
                Text = _labelText
            },
            For = _labelFor
        },
        Error = new()
        {
            ErrorMessage = new()
            {
                Text = _errorMessage
            }
        }
    };

    public IGDSCheckboxBuilder SetCheckbox(Action<ICheckboxBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        configure(_checkboxBuilder);
        return this;
    }

    public IGDSCheckboxBuilder SetCheckbox(CheckboxComponent checkbox)
    {
        ArgumentNullException.ThrowIfNull(checkbox);
        _checkboxBuilder
            .SetChecked(checkbox.IsChecked)
            .SetId(checkbox.Id)
            .SetName(checkbox.Name)
            .SetRequired(checkbox.IsRequired)
            .SetValue(checkbox.Value);
        return this;
    }

    public IGDSCheckboxBuilder SetErrorMessage(string error)
    {
        ArgumentNullException.ThrowIfNull(error);
        _errorMessage = error;
        return this;
    }

    public IGDSCheckboxBuilder SetLabelFor(string labelFor)
    {
        ArgumentNullException.ThrowIfNull(labelFor);
        _labelFor = labelFor;
        return this;
    }

    public IGDSCheckboxBuilder SetLabelText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        _labelText = text;
        return this;
    }
}
