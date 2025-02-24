using Dfe.Testing.Pages.Components.AnchorLink;
using Dfe.Testing.Pages.Components.Form;
using Dfe.Testing.Pages.Contracts.PageObjectClient.Request;
using Dfe.Testing.Pages.Contracts.PageObjectClient.Response;
using Dfe.Testing.Pages.Contracts.PageObjectClient.Templates;

namespace Dfe.Testing.Pages.Components.CookieChoiceBanner;

public record CookieChoiceMadeBannerComponent
{
    public FormComponent? HideCookiesForm { get; init; } = new();
    public AnchorLinkComponent? ChangeYourCookieSettingsLink { get; init; } = new();
    public string? Content { get; init; } = string.Empty;
}

public sealed class CookieChoiceMadeBannerPropertyOptions
{
    private readonly FormPageOptions _options;
    private const string ROOT = "Banner";
    public CookieChoiceMadeBannerPropertyOptions(FormPageOptions options)
    {
        _options = options;
    }

    public string Banner => "Banner";
    public string Content => $"{ROOT}.Content";
    public FormPageOptions Form { get => _options; }
    public string ChangeCookiesLink => $"{ROOT}.ChangeCookiesLink";
}

public sealed class CookieChoiceMadeBannerMapper : IMapper<PageObjectResponse, CookieChoiceMadeBannerComponent>
{
    private readonly CookieChoiceMadeBannerPropertyOptions _options;
    private readonly IMapper<CreatedPageObjectModel, FormComponent> _formMapper;
    private readonly IMapper<CreatedPageObjectModel, AnchorLinkComponent> _linkMapper;

    public CookieChoiceMadeBannerMapper(
        CookieChoiceMadeBannerPropertyOptions options,
        IMapper<CreatedPageObjectModel, FormComponent> formMapper,
        IMapper<CreatedPageObjectModel, AnchorLinkComponent> linkMapper)
    {
        _options = options;
        _formMapper = formMapper;
        _linkMapper = linkMapper;
    }

    public CookieChoiceMadeBannerComponent Map(PageObjectResponse input)
    {
        var banner = input.Created.Single(t => t.Id == _options.Banner);

        return new()
        {
            ChangeYourCookieSettingsLink =
                banner.Children
                    .Where(t => t.Id == _options.ChangeCookiesLink)
                    .Select(_linkMapper.Map)
                    .SingleOrDefault(),
            Content =
                banner.Children
                    .Where(t => t.Id == _options.Content)
                    .Select(t => t.GetAttribute("text"))
                    .SingleOrDefault(),
            HideCookiesForm = _formMapper.Map(
                banner.Children
                    .Single(t => t.Id == _options.Form.Form))
        };
    }
}

internal sealed class CookieChoiceMadeBannerPageObjectTemplate : IPageObjectTemplate
{
    private readonly CookieChoiceMadeBannerPropertyOptions _options;
    private readonly FormPageOptions _formOptions;

    public CookieChoiceMadeBannerPageObjectTemplate(
        CookieChoiceMadeBannerPropertyOptions options,
        FormPageOptions formOptions)
    {
        _options = options;
        _formOptions = formOptions;
    }

    public string Id => nameof(CookieChoiceMadeBannerComponent);

    public PageObjectSchema Schema =>
        new()
        {
            Id = _options.Banner,
            Find = ".govuk-cookie-banner",
            Children = [
                new PageObjectSchema()
                {
                    Id = _options.Content,
                    Find = "form p",
                },
                new PageObjectSchema()
                {
                    Id = _options.ChangeCookiesLink,
                    Find = "a",
                },
                new FormTemplate(_formOptions).Schema
            ]
        };
}
