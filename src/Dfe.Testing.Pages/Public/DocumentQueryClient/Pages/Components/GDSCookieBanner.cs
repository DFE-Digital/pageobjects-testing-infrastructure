namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;
public record GDSCookieBanner : IComponent
{
    public required string Heading { get; init; }
    //public required string Content { get; init; }
    public required IEnumerable<GDSButton> CookieChoiceButtons { get; init; }
    public required AnchorLink ViewCookiesLink { get; init; }
    public string TagName { get; init; } = "div";
}
