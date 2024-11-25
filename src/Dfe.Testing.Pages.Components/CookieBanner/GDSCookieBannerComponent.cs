using Dfe.Testing.Pages.Components.AnchorLink;
using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Public.DocumentQueryClient;

namespace Dfe.Testing.Pages.Components.CookieBanner;
public record GDSCookieBannerComponent : IComponent
{
    public required string Heading { get; init; }
    //public required string Content { get; init; }
    public required IEnumerable<GDSButtonComponent> CookieChoiceButtons { get; init; }
    public required AnchorLinkComponent ViewCookiesLink { get; init; }
    public string TagName { get; init; } = "div";
}
