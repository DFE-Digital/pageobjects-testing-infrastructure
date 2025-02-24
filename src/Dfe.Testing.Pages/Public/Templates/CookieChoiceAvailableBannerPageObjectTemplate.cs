using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Templates;

public sealed class CookieChoiceAvailableBannerPropertyOptions
{
    private const string Root = "CookieChoiceAvailableBanner";
    private readonly FormPageOptions _formOptions;
    public CookieChoiceAvailableBannerPropertyOptions(FormPageOptions formOptions)
    {
        _formOptions = formOptions;
        CookieChoiceForm = _formOptions.Form;
        CookieChoiceFormButtons = _formOptions.Buttons;
    }

    public string Banner { get; set; } = Root;
    public string Heading { get; set; } = $"{Root}.Heading";
    public string Content { get; set; } = $"{Root}.Content";
    public string ViewCookiesLink { get; set; } = $"{Root}.ViewCookies";
    public string CookieChoiceForm { get; set; }
    public string CookieChoiceFormButtons { get; set; }
}

public record CookieChoiceAvailableBannerComponent
{
    public string? Heading { get; set; }
    public string? Content { get; set; }
    public AnchorLinkComponent? ViewCookies { get; set; }
    public FormComponent? ChoiceForm { get; set; }
}

public sealed class CookieChoiceAvailableBannerPageObjectResponseMapper : IMapper<PageObjectResponse, CookieChoiceAvailableBannerComponent>
{
    private readonly CookieChoiceAvailableBannerPropertyOptions _options;
    private readonly IMapper<CreatedPageObjectModel, AnchorLinkComponent> _linkMapper;
    private readonly IMapper<CreatedPageObjectModel, ButtonComponent> _buttonMapper;
    private readonly IMapper<CreatedPageObjectModel, FormComponent> _formMapper;

    public CookieChoiceAvailableBannerPageObjectResponseMapper(
        CookieChoiceAvailableBannerPropertyOptions options,
        IMapper<CreatedPageObjectModel, AnchorLinkComponent> linkMapper,
        IMapper<CreatedPageObjectModel, ButtonComponent> buttonMapper,
        IMapper<CreatedPageObjectModel, FormComponent> formMapper)
    {
        _options = options;
        _linkMapper = linkMapper;
        _buttonMapper = buttonMapper;
        _formMapper = formMapper;
    }

    public CookieChoiceAvailableBannerComponent Map(PageObjectResponse input)
    {
        CreatedPageObjectModel banner = input.Created.Single(t => t.Id == _options.Banner);

        // heading
        CreatedPageObjectModel heading = banner.Children.Single(t => t.Id == _options.Heading);

        // content
        CreatedPageObjectModel content = banner.Children.Single(t => t.Id == _options.Content);

        // view cookies link
        CreatedPageObjectModel viewCookies = banner.Children.Single(t => t.Id == _options.ViewCookiesLink);
        AnchorLinkComponent? viewCookiesLink = _linkMapper.Map(viewCookies);

        // form
        CreatedPageObjectModel form = banner.Children.Single(t => t.Id == _options.CookieChoiceForm);
        FormComponent choiceForm = _formMapper.Map(form);

        return new()
        {
            Heading = heading.GetAttribute("text"),
            Content = content.GetAttribute("text"),
            ViewCookies = viewCookiesLink,
            ChoiceForm = choiceForm
        };
    }
}

public sealed class CookieChoiceAvailableBannerPageObjectTemplate : IPageObjectTemplate
{
    private readonly CookieChoiceAvailableBannerPropertyOptions _options;

    public CookieChoiceAvailableBannerPageObjectTemplate(CookieChoiceAvailableBannerPropertyOptions options)
    {
        _options = options;
    }

    public string Id => nameof(CookieChoiceAvailableBannerComponent);

    public PageObjectSchema Schema =>
            new()
            {
                Id = _options.Banner,
                Find = ".govuk-cookie-banner",
                Children = [
                    new PageObjectSchema()
                    {
                        Id = _options.Heading,
                        Find = ".govuk-cookie-banner__heading",
                    },
                    new PageObjectSchema()
                    {
                        Id = _options.Content,
                        Find = ".govuk-cookie-banner__content",
                    },
                    new PageObjectSchema()
                    {
                        Id = _options.CookieChoiceForm,
                        Find = "form",
                        Children = [
                            new PageObjectSchema()
                            {
                                Id = _options.CookieChoiceFormButtons,
                                Find = "button"
                            }
                        ]
                    },
                    new PageObjectSchema()
                    {
                        Id = _options.ViewCookiesLink,
                        Find = "a"
                    }
                ]
            };
}

