using Dfe.Testing.Pages.Components.NotificationBanner;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSNotificationBannerMapper : IComponentMapper<GDSNotificationBannerComponent>
{
    public GDSNotificationBannerComponent Map(IDocumentPart input)
    {
        return new()
        {
            Heading = input.GetChild(new CssSelector(".govuk-notification-banner__title"))?.Text ??
                throw new ArgumentNullException("heading on notification banner is null"),

            Content = input.GetChild(new CssSelector(".govuk-notification-banner__content"))?.Text ??
                throw new ArgumentNullException("content on notification banner is null")
        };
    }
}
