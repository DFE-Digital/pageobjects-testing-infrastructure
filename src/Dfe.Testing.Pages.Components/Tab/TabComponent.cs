using Dfe.Testing.Pages.Components.AnchorLink;
using Dfe.Testing.Pages.Contracts.PageObjectClient.Request;
using Dfe.Testing.Pages.Contracts.PageObjectClient.Response;
using Dfe.Testing.Pages.Contracts.PageObjectClient.Templates;

namespace Dfe.Testing.Pages.Components.Tab;

public record GdsTabsComponent
{
    public IEnumerable<AnchorLinkComponent> Tabs { get; init; } = [];
}

public sealed class GdsTabsOptions
{
    private const string ROOT = "GDSTabs";
    public string TabContainer { get; set; } = ROOT;
    public string Tabs { get; set; } = $"{ROOT}.Tabs";
}


internal sealed class GdsTabMapper : IMapper<PageObjectResponse, GdsTabsComponent>
{
    private readonly GdsTabsOptions _options;
    private readonly IMapper<CreatedPageObjectModel, AnchorLinkComponent> _linkMapper;

    public GdsTabMapper(
        GdsTabsOptions options,
        IMapper<CreatedPageObjectModel, AnchorLinkComponent> linkMapper)
    {
        _options = options;
        _linkMapper = linkMapper;
    }

    public GdsTabsComponent Map(PageObjectResponse input)
    {
        return new GdsTabsComponent()
        {
            Tabs = input?.Created
                .Single(t => t.Id == _options.TabContainer)
                .Children
                .Where(t => t.Id == _options.Tabs)
                .Select(_linkMapper.Map) ?? []
        };
    }
}

internal sealed class GdsTabsComponentTemplate : IPageObjectTemplate
{
    private readonly GdsTabsOptions _options;

    public GdsTabsComponentTemplate(GdsTabsOptions options)
    {
        _options = options;
    }

    public string Id => nameof(GdsTabsComponent);

    public PageObjectSchema Schema =>
        new()
        {
            Id = _options.TabContainer,
            Find = ".govuk-tabs",
            Children = [
                new PageObjectSchema()
                {
                    Id = _options.Tabs,
                    Find = ".govuk-tabs__tab"
                }
            ]
        };
}
