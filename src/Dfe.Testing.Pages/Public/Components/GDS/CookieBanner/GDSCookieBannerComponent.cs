using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
public record GDSCookieBannerComponent : IComponent
{
    public required string Heading { get; init; }
    //public required string Content { get; init; }
    public required IEnumerable<GDSButtonComponent> CookieChoiceButtons { get; init; }
    public required AnchorLinkComponent ViewCookiesLink { get; init; }
    public string TagName { get; init; } = "div";
}
