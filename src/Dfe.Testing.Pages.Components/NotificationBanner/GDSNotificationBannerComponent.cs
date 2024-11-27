namespace Dfe.Testing.Pages.Components.NotificationBanner;
public record GDSNotificationBannerComponent : IComponent
{
    public required string Heading { get; init; }
    public required string Content { get; init; }
}
