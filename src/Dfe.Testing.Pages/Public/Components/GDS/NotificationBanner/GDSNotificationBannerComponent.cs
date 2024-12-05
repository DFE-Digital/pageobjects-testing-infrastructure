namespace Dfe.Testing.Pages.Public.Components.GDS.NotificationBanner;
public record GDSNotificationBannerComponent : IComponent
{
    public required string Heading { get; init; }
    public required string Content { get; init; }
}
