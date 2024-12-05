namespace Dfe.Testing.Pages.Public.Components.GDS.NotificationBanner;
internal sealed class GDSNotificationBannerMapper : IComponentMapper<GDSNotificationBannerComponent>
{
    public GDSNotificationBannerComponent Map(IDocumentPart input)
    {
        return new()
        {
            Heading = input.FindDescendant(new CssElementSelector(".govuk-notification-banner__title"))?.Text ??
                throw new ArgumentNullException("heading on notification banner is null"),

            Content = input.FindDescendant(new CssElementSelector(".govuk-notification-banner__content"))?.Text ??
                throw new ArgumentNullException("content on notification banner is null")
        };
    }
}
