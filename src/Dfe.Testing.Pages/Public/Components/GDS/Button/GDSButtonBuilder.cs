using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Button;

internal sealed class GDSButtonBuilder : IGDSButtonBuilder
{
    private string _name;
    private string _value;
    private string _text;
    private string _type;
    private bool _enabled;
    private ButtonStyleType _buttonType;

    public GDSButtonBuilder()
    {
        _name = string.Empty;
        _value = string.Empty;
        _text = string.Empty;
        _type = string.Empty;
        _enabled = true;
        _buttonType = ButtonStyleType.Primary;
    }

    public GDSButtonComponent Build() => new()
    {
        Text = new TextComponent()
        {
            Text = _text
        },
        ButtonStyle = _buttonType,
        Type = _type,
        Name = _name,
        Value = _value,
        IsEnabled = _enabled
    };

    public IGDSButtonBuilder SetType(string type)
    {
        ArgumentNullException.ThrowIfNull(type);
        _type = type;
        return this;
    }

    public IGDSButtonBuilder SetName(string name)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        _name = name;
        return this;
    }

    public IGDSButtonBuilder SetText(string text)
    {
        ArgumentNullException.ThrowIfNull(text, nameof(text));
        _text = text;
        return this;
    }

    public IGDSButtonBuilder SetValue(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _value = value;
        return this;
    }

    public IGDSButtonBuilder SetButtonStyle(ButtonStyleType buttonType)
    {
        _buttonType = buttonType;
        return this;
    }

    public IGDSButtonBuilder SetEnabled(bool enabled)
    {
        _enabled = enabled;
        return this;
    }
}
