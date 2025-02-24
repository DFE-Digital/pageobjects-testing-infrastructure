using Dfe.Testing.Pages.Components.AnchorLink;
using Dfe.Testing.Pages.Components.Form;
using Dfe.Testing.Pages.Public.PageObjects;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Request;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Response;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Templates;

namespace Dfe.Testing.Pages.Components.CookieChoiceBanner;

public sealed class CookieChoiceAvailableBannerPropertyOptions
{
    private const string Root = "CookieChoiceAvailableBanner";
    private readonly FormPageOptions _formOptions;
    public CookieChoiceAvailableBannerPropertyOptions(FormPageOptions formOptions)
    {
        _formOptions = formOptions;
    }

    public string Banner => Root;
    public string Heading => $"{Root}.Heading";
    public string Content => $"{Root}.Content";
    public string ViewCookiesLink => $"{Root}.ViewCookies";
    public FormPageOptions CookieChoiceForm { get => _formOptions; }
}

public record CookieChoiceAvailableBannerComponent
{
    public string? Heading { get; set; }
    public string? Content { get; set; }
    public AnchorLinkComponent? ViewCookies { get; set; } = new()
    {
        Link = string.Empty,
        Text = "View cookies",
    };
    public FormComponent? ChoiceForm { get; set; } = new()
    {
        Action = string.Empty,
        Method = HttpMethod.Post,
        Buttons = [
            new ButtonComponent()
            {
                Text = "Accept analytics cookies",
                Value = "true"
            },
            new ButtonComponent()
            {
                Text = "Reject analytics cookies",
                Value = "true"
            }
        ]
    };
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
        var banner = input.Created.Single(t => t.Id == _options.Banner);

        // heading
        CreatedPageObjectModel heading = banner.Children.Single(t => t.Id == _options.Heading);

        // content
        CreatedPageObjectModel content = banner.Children.Single(t => t.Id == _options.Content);

        // view cookies link
        CreatedPageObjectModel viewCookies = banner.Children.Single(t => t.Id == _options.ViewCookiesLink);
        var viewCookiesLink = _linkMapper.Map(viewCookies);

        // form
        CreatedPageObjectModel form = banner.Children.Single(t => t.Id == _options.CookieChoiceForm.Form);
        var choiceForm = _formMapper.Map(form);

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
    private readonly FormPageOptions _formOptions;
    private readonly CookieChoiceAvailableBannerPropertyOptions _options;

    public CookieChoiceAvailableBannerPageObjectTemplate(
        FormPageOptions formOptions,
        CookieChoiceAvailableBannerPropertyOptions options)
    {
        _formOptions = formOptions;
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
                    new FormTemplate(_formOptions).Schema,
                    new PageObjectSchema()
                    {
                        Id = _options.ViewCookiesLink,
                        Find = "a"
                    }
                ]
            };
}

