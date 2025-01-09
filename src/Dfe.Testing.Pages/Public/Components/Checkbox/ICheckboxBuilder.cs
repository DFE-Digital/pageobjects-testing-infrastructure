namespace Dfe.Testing.Pages.Public.Components.Checkbox;
internal sealed class CheckboxBuilder : ICheckboxBuilder
{
    private string _id = string.Empty;
    private string _name = string.Empty;
    private string _value = string.Empty;
    private bool _checked = false;
    private bool _isRequired = false;

    public CheckboxComponent Build() => new()
    {
        Name = _name,
        Value = _value,
        Id = _id,
        IsChecked = _checked,
        IsRequired = _isRequired
    };

    public ICheckboxBuilder SetChecked(bool isChecked)
    {
        _checked = isChecked;
        return this;
    }

    public ICheckboxBuilder SetId(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        _id = id;
        return this;
    }

    public ICheckboxBuilder SetName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        _name = name;
        return this;
    }

    public ICheckboxBuilder SetRequired(bool isRequired)
    {
        _isRequired = isRequired;
        return this;
    }

    public ICheckboxBuilder SetValue(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _value = value;
        return this;
    }
}
