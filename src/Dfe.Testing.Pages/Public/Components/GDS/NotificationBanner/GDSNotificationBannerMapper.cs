using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.NotificationBanner;
internal sealed class GDSNotificationBannerMapper : BaseDocumentSectionToComponentMapper<GDSNotificationBannerComponent>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;

    public GDSNotificationBannerMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TextComponent> textMapper) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        _textMapper = textMapper;
    }

    public override GDSNotificationBannerComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<GDSNotificationBannerComponent>(input);
        return new()
        {
            Heading = _documentSectionFinder.Find(mappable, new CssElementSelector(".govuk-notification-banner__title")).MapWith(_textMapper),
            Content = _documentSectionFinder.Find(mappable, new CssElementSelector(".govuk-notification-banner__content")).MapWith(_textMapper)
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => true; // TODO notification banner
}
