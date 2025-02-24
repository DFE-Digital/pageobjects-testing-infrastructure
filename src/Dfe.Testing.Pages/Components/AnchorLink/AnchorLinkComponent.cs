using Dfe.Testing.Pages.Public.PageObjects;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Response;

namespace Dfe.Testing.Pages.Components.AnchorLink;
public record AnchorLinkComponent
{
    public static readonly string[] RelKnownAttributes = ["noopener", "noreferrer", "nofollow"];
    public string? Link { get; init; }
    public string? Text { get; init; }
    public bool OpensInNewTab { get; init; } = false;
    public string? Rel { get; init; }
}

public sealed class AnchorLinkComponentMapper : IMapper<CreatedPageObjectModel, AnchorLinkComponent>
{
    public AnchorLinkComponent Map(CreatedPageObjectModel input)
    {
        return new()
        {
            Link = input.GetAttribute("href"),
            Text = input.GetAttribute("text"),
            OpensInNewTab = input.GetAttribute("target") == "_blank",
            Rel = input.GetAttribute("rel")
        };
    }
}
