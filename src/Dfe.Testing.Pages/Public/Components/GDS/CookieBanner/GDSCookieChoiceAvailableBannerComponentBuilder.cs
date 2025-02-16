/*using Dfe.Testing.Pages.Public.Components.Form;
using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;
*/

using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using Dfe.Testing.Pages.Public.Components.HiddenInput;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceAvailableBannerComponentBuilder : IGDSCookieChoiceAvailableBannerComponentBuilder
{
    private string _heading = string.Empty;
    private AnchorLinkComponentOld? anchorLinkComponent = null!;
    private FormComponentOld? _form;
    private readonly List<GDSButtonComponent> _cookieChoiceButtons = [];
    private readonly IAnchorLinkComponentBuilder _anchorLinkBuilder;
    private readonly IFormBuilder _formBuilder;

    public GDSCookieChoiceAvailableBannerComponentBuilder(
        IAnchorLinkComponentBuilder anchorLink,
        IFormBuilder builder)
    {
        _anchorLinkBuilder = anchorLink;
        _formBuilder = builder;
    }
    public GDSCookieChoiceAvailableBannerComponent Build()
    {
        return new()
        {
            CookieChoiceForm = _form ?? _formBuilder.Build(),
            Heading = new()
            {
                Text = _heading
            },
            ViewCookiesLink = anchorLinkComponent ?? _anchorLinkBuilder.Build()
        };
    }

    public IGDSCookieChoiceAvailableBannerComponentBuilder SetForm(FormComponentOld form)
    {
        ArgumentNullException.ThrowIfNull(form);
        _form = form;
        return this;
    }

    public IGDSCookieChoiceAvailableBannerComponentBuilder SetViewCookiesLink(AnchorLinkComponentOld link)
    {
        ArgumentNullException.ThrowIfNull(link);
        anchorLinkComponent = link;
        return this;
    }

    public IGDSCookieChoiceAvailableBannerComponentBuilder SetHeading(string heading)
    {
        ArgumentNullException.ThrowIfNull(heading);
        _heading = heading;
        return this;
    }
}

public interface IAnchorLinkComponentBuilder
{
    AnchorLinkComponentOld Build();
    IAnchorLinkComponentBuilder SetLink(string linkedTo);
    IAnchorLinkComponentBuilder SetText(string text);
    IAnchorLinkComponentBuilder SetOpensInNewTab(bool opensInNewTab);
    IAnchorLinkComponentBuilder AddSecurityRelAttribute(string attribute);
    IAnchorLinkComponentBuilder AddSecurityRelAttribute(IEnumerable<string> attributes);
}

internal sealed class AnchorLinkComponentBuilder : IAnchorLinkComponentBuilder
{
    private readonly List<string> _relAttributes;
    private string _link;
    private string _text;
    private bool _opensInNewTab;

    public AnchorLinkComponentBuilder()
    {
        _relAttributes = [];
        _link = string.Empty;
        _text = string.Empty;
        _opensInNewTab = false;
    }
    public AnchorLinkComponentOld Build() => new()
    {
        Link = _link,
        OpensInNewTab = _opensInNewTab,
        Text = new TextComponent
        {
            Text = _text
        },
        NewText = "",
        RelAttributes = _relAttributes
    };

    public IAnchorLinkComponentBuilder SetLink(string link)
    {
        ArgumentNullException.ThrowIfNull(link);
        _link = link;
        return this;
    }

    public IAnchorLinkComponentBuilder SetText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        _text = text;
        return this;
    }

    public IAnchorLinkComponentBuilder SetOpensInNewTab(bool opensInNewTab)
    {
        _opensInNewTab = opensInNewTab;
        return this;
    }

    public IAnchorLinkComponentBuilder AddSecurityRelAttribute(string attribute)
    {
        ArgumentException.ThrowIfNullOrEmpty(attribute);
        if (!AnchorLinkComponentOld.RelKnownAttributes.Contains(attribute, StringComparer.OrdinalIgnoreCase))
        {
            throw new ArgumentException($"Unknown rel attribute {attribute}.");
        }
        _relAttributes.Add(attribute);
        return this;
    }

    public IAnchorLinkComponentBuilder AddSecurityRelAttribute(IEnumerable<string> attributes)
    {
        attributes.ToList().ForEach(t => AddSecurityRelAttribute(t));
        return this;
    }
}


public interface IFormBuilder
{
    FormComponentOld Build();
    public IFormBuilder SetMethod(System.Net.Http.HttpMethod method);
    public IFormBuilder SetMethod(string method);
    public IFormBuilder SetAction(string action);
    public IFormBuilder SetIsFormHTMLValidated(bool validated);
    public IFormBuilder AddButton(Action<IGDSButtonBuilder>? configureButton = null);
    public IFormBuilder AddButton(GDSButtonComponent button);
    public IFormBuilder AddCheckbox(Action<IGDSCheckboxBuilder>? confiureCheckbox = null);
    public IFormBuilder AddCheckbox(GDSCheckboxComponent checkbox);
}

public record AnchorLinkComponentOld
{
    public static readonly string[] RelKnownAttributes = ["noopener", "noreferrer", "nofollow"];
    public required string Link { get; init; } = string.Empty;
    public required string NewText { get; init; } = string.Empty;
    public bool OpensInNewTab { get; init; } = false;
    public required TextComponent? Text { get; init; }
    public IEnumerable<string> RelAttributes { get; init; } = [];
}

internal sealed class FormBuilder : IFormBuilder
{
    private string _action = string.Empty;
    private System.Net.Http.HttpMethod _method = System.Net.Http.HttpMethod.Get;
    private bool _isFormValidated = true;
    private readonly IGDSButtonBuilder _buttonBuilder;
    private readonly List<GDSButtonComponent> _buttons = [];

    public FormBuilder(IGDSButtonBuilder buttonBuilder)
    {
        _buttonBuilder = buttonBuilder;
    }
    public FormComponentOld Build()
    {
        return new()
        {
            Action = _action,
            Method = _method,
            Buttons = _buttons,
            Fieldsets = [],
            Checkboxes = [],
            Radios = [],
            TextInputs = [],
            HiddenInputs = [],
            Selects = []
        };
    }

    public IFormBuilder AddButton(GDSButtonComponent button)
    {
        ArgumentNullException.ThrowIfNull(button);
        _buttons.Add(button);
        return this;
    }

    public IFormBuilder AddButton(Action<IGDSButtonBuilder>? configureButton = null)
    {
        configureButton?.Invoke(_buttonBuilder);
        GDSButtonComponent button = _buttonBuilder.Build();
        _buttons.Add(button);
        return this;
    }

    public IFormBuilder SetAction(string action)
    {
        ArgumentNullException.ThrowIfNull(action);
        _action = action;
        return this;
    }

    public IFormBuilder SetIsFormHTMLValidated(bool validated)
    {
        _isFormValidated = validated;
        return this;
    }

    public IFormBuilder SetMethod(System.Net.Http.HttpMethod method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _method = method;
        return this;
    }

    public IFormBuilder SetMethod(string method)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(method);
        _method = System.Net.Http.HttpMethod.Parse(method);
        return this;
    }

    public IFormBuilder AddCheckbox(Action<IGDSCheckboxBuilder>? confiureCheckbox = null)
    {
        throw new NotImplementedException();
    }

    public IFormBuilder AddCheckbox(GDSCheckboxComponent checkbox)
    {
        throw new NotImplementedException();
    }
}

public record FormComponentOld
{
    internal FormComponentOld() { }
    public required System.Net.Http.HttpMethod Method { get; init; }
    public string Action { get; init; } = string.Empty;
    public bool IsFormValidated { get; init; } = true;
    public required IEnumerable<GDSFieldsetComponent?> Fieldsets { get; init; } = [];
    public required IEnumerable<GDSButtonComponent?> Buttons { get; init; } = [];
    public IEnumerable<GDSCheckboxComponent?> Checkboxes { get; init; } = [];
    public IEnumerable<GDSRadioComponent?> Radios { get; init; } = [];
    public IEnumerable<GDSTextInputComponent?> TextInputs { get; init; } = [];
    public IEnumerable<GDSSelectComponent?> Selects { get; init; } = [];
    public IEnumerable<HiddenInputComponent?> HiddenInputs { get; init; } = [];
}

