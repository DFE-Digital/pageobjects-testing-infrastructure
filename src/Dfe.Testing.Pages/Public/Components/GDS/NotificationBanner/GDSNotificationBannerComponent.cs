using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.NotificationBanner;
public record GDSNotificationBannerComponent
{
    public required TextComponent? Heading { get; init; }
    public required TextComponent? Content { get; init; }
}
