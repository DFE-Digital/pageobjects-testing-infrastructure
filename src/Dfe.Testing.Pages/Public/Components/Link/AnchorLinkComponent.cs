using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.Link;
public record AnchorLinkComponent
{
    public static readonly string[] RelKnownAttributes = ["noopener", "noreferrer"];
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
    IAnchorLinkComponentBuilder AddRelAttribute(string attribute);
}

internal sealed class AnchorLinkComponentBuilder : IAnchorLinkComponentBuilder
{
    private readonly List<string> _relAttributes = [];
    private string _link = string.Empty;
    private string _text = string.Empty;
    private bool _opensInNewTab = false;

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

    public IAnchorLinkComponentBuilder AddRelAttribute(string attribute)
    {
        ArgumentException.ThrowIfNullOrEmpty(attribute);
        if (!AnchorLinkComponent.RelKnownAttributes.Contains(attribute, StringComparer.OrdinalIgnoreCase))
        {
            throw new ArgumentException($"Unknown rel attribute {attribute}.");
        }
        _relAttributes.Add(attribute);
        return this;
    }
}
