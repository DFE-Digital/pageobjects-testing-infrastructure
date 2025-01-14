using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.Link;
public record AnchorLinkComponent
{
    public static readonly string[] RelKnownAttributes = ["noopener", "noreferrer", "nofollow"];
    internal AnchorLinkComponent() { }
    public required string Link { get; init; }
    public bool OpensInNewTab { get; init; } = false;
    public required TextComponent? Text { get; init; }
    public IEnumerable<string> RelAttributes { get; init; } = [];
}

public interface IAnchorLinkComponentBuilder
{
    AnchorLinkComponent Build();
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
    public AnchorLinkComponent Build() => new()
    {
        Link = _link,
        OpensInNewTab = _opensInNewTab,
        Text = new TextComponent
        {
            Text = _text
        },
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
        if (!AnchorLinkComponent.RelKnownAttributes.Contains(attribute, StringComparer.OrdinalIgnoreCase))
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
