using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.PhaseBanner;
public record GDSPhaseBannerComponent
{
    public required AnchorLinkComponent FeedbackLink { get; init; }
    public required TextComponent Text { get; init; }
    public required TextComponent Phase { get; init; }
}

public interface IGDSPhaseBannerBuilder
{
    IGDSPhaseBannerBuilder SetPhase(string phase);
    IGDSPhaseBannerBuilder SetBannerText(string text);
    IGDSPhaseBannerBuilder SetFeedbackLink(AnchorLinkComponent link);
    GDSPhaseBannerComponent Build();
}


internal sealed class GDSPhaseBannerBuilder : IGDSPhaseBannerBuilder
{
    private string _phase;
    private string _bannerText;
    private AnchorLinkComponent? _feedbackLink;

    public GDSPhaseBannerBuilder()
    {
        _phase = string.Empty;
        _bannerText = string.Empty;
    }

    public GDSPhaseBannerComponent Build() => new()
    {
        Phase = new TextComponent
        {
            Text = _phase
        },
        Text = new TextComponent
        {
            Text = _bannerText
        },
        FeedbackLink = _feedbackLink ?? throw new ArgumentException("feedback link has not been set")
    };

    public IGDSPhaseBannerBuilder SetPhase(string phase)
    {
        ArgumentNullException.ThrowIfNull(phase);
        _phase = phase;
        return this;
    }

    public IGDSPhaseBannerBuilder SetBannerText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        _bannerText = text;
        return this;
    }

    public IGDSPhaseBannerBuilder SetFeedbackLink(AnchorLinkComponent link)
    {
        ArgumentNullException.ThrowIfNull(link);
        _feedbackLink = link;
        return this;
    }
}
