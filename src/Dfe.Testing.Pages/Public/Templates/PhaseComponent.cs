
namespace Dfe.Testing.Pages.Public.Templates;
public record PhaseBannerComponent
{
    public string? Phase { get; init; }
    public AnchorLinkComponent? FeedbackForm { get; init; }
    public string? Text { get; init; }
}

public sealed class PhaseBannerComponentMapper : IMapper<CreatedPageObjectModel, PhaseBannerComponent>
{
    public PhaseBannerComponent Map(CreatedPageObjectModel input)
    {
        var feedbackLinkAttributes = input.GetMappedProperty("FeedbackLink").Single();
        return new PhaseBannerComponent()
        {
            Phase = input.GetMappedProperty("Phase").Single().TryGetOrDefault("text") ?? string.Empty,
            Text = input.GetMappedProperty("BannerText").Single().TryGetOrDefault("text") ?? string.Empty,
            FeedbackForm = new AnchorLinkComponent()
            {
                Link = feedbackLinkAttributes.TryGetOrDefault("href"),
                Text = feedbackLinkAttributes.TryGetOrDefault("text"),
                OpensInNewTab = feedbackLinkAttributes.TryGetOrDefault("target") == "_blank"
            }
        };
    }
}

public sealed class PhaseBannerComponentTemplate : IPageObjectTemplate
{
    public string TemplateId => "PhaseBanner";

    public QueryOptions? Query => new()
    {
        Find = ".govuk-phase-banner",
    };

    public IEnumerable<PageObjectSchema> PageObjects => [
        new()
        {
            Id = "Banner",
            Properties = [
                new()
                {
                    Attributes = [],
                    MappingEntrypoint = ".govuk-phase-banner__content__tag",
                    ToProperty = "Phase"
                },
                new()
                {
                    Attributes = [],
                    MappingEntrypoint = ".govuk-phase-banner__text",
                    ToProperty = "BannerText"
                },
                new()
                {
                    Attributes = [],
                    MappingEntrypoint = ".govuk-phase-banner__text a",
                    ToProperty = "FeedbackLink"
                },
            ]
        }
        ];
}
