using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Templates;

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
    public string Content => "Content";
    public string Form { get => _options.Form; }
    public string ChangeCookiesLink => "ChangeCookiesLink";
}

public sealed class CookieChoiceMadeBannerMapper : IMapper<PageObjectResponse, CookieChoiceMadeBannerComponent>
{
    private readonly CookieChoiceMadeBannerPropertyOptions _options;
    private readonly IMapper<CreatedPageObjectModel, FormComponent> _formMapper;
    private readonly IMapper<CreatedPageObjectModel, AnchorLinkComponent> _linkMapper;
    private readonly IMapper<CreatedPageObjectModel, ButtonComponent> _buttonMapper;

    public CookieChoiceMadeBannerMapper(
        CookieChoiceMadeBannerPropertyOptions options,
        IMapper<CreatedPageObjectModel, FormComponent> formMapper,
        IMapper<CreatedPageObjectModel, AnchorLinkComponent> linkMapper,
        IMapper<CreatedPageObjectModel, ButtonComponent> buttonMapper)
    {
        _options = options;
        _formMapper = formMapper;
        _linkMapper = linkMapper;
        _buttonMapper = buttonMapper;
    }

    public CookieChoiceMadeBannerComponent Map(PageObjectResponse input)
    {
        CreatedPageObjectModel banner = input.Created.Single(t => t.Id == _options.Banner);

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
                    .Single(t => t.Id == _options.Form))
        };
    }
}

internal sealed class CookieChoiceMadeBannerPageObjectTemplate : IPageObjectTemplate
{
    private readonly FormTemplate _formTemplate;
    private readonly CookieChoiceMadeBannerPropertyOptions _options;
    public CookieChoiceMadeBannerPageObjectTemplate(
        FormTemplate formTemplate,
        CookieChoiceMadeBannerPropertyOptions options)
    {
        _formTemplate = formTemplate;
        _options = options;
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
                _formTemplate.Schema
            ]
        };
}
