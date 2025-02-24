
namespace Dfe.Testing.Pages.Public.Templates;
public record PhaseBannerComponent
{
    public string? Phase { get; init; }
    public AnchorLinkComponent? FeedbackLink { get; init; }
    public string? Text { get; init; }
}

public sealed class PhaseBannerPageObjectPropertyOptions
{
    public string Banner { get; set; } = "Banner";
    public string Phase { get; set; } = "Banner.PhaseTag";
    public string BannerText { get; set; } = "Banner.BannerText";
    public string FeedbackLink { get; set; } = "Banner.FeedbackLink";
}

internal sealed class PhaseBannerComponentMapper : IMapper<PageObjectResponse, PhaseBannerComponent>
{
    private readonly PhaseBannerPageObjectPropertyOptions _options;
    private readonly IMapper<CreatedPageObjectModel, AnchorLinkComponent> _linkMapper;

    public PhaseBannerComponentMapper(
        PhaseBannerPageObjectPropertyOptions options,
        IMapper<CreatedPageObjectModel, AnchorLinkComponent> linkMapper)
    {
        _options = options;
        _linkMapper = linkMapper;
    }

    public PhaseBannerComponent Map(PageObjectResponse mapFrom)
    {
        var phaseBannerModel = mapFrom.Created.Single(t => t.Id == _options.Banner);
        return new()
        {
            Phase =
                phaseBannerModel.Children
                    .Single(t => _options.Phase == t.Id)
                    .GetAttribute("text"),
            Text =
                phaseBannerModel.Children
                    .Single(t => _options.BannerText == t.Id)
                    .GetAttribute("text"),
            FeedbackLink =
                _linkMapper.Map(
                    phaseBannerModel.Children.Single(t => _options.FeedbackLink == t.Id))
        };
    }
}

internal sealed class PhaseBannerComponentTemplate : IPageObjectTemplate
{
    private readonly PhaseBannerPageObjectPropertyOptions _options;
    public string Id => nameof(PhaseBannerComponent);
    public PhaseBannerComponentTemplate(PhaseBannerPageObjectPropertyOptions option)
    {
        _options = option;
    }

    public PageObjectSchema Schema => new()
    {
        Id = _options.Banner,
        Find = ".govuk-phase-banner",
        Children = [
            new()
            {
                Id = _options.FeedbackLink,
                Find = ".govuk-phase-banner__text a"
            },
            new()
            {
                Id = _options.BannerText,
                Find = ".govuk-phase-banner__text",
            },
            new()
            {
                Id = _options.Phase,
                Find = ".govuk-phase-banner__content__tag",
            }
        ]
    };
}
