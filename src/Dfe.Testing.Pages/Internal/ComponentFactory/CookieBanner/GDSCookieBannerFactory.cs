using Dfe.Testing.Pages.Components.CookieBanner;

namespace Dfe.Testing.Pages.Internal.ComponentFactory.CookieBanner;
internal sealed class GDSCookieBannerFactory : ComponentFactory<GDSCookieBannerComponent>
{
    private readonly IComponentMapper<GDSCookieBannerComponent> _mapper;

    public GDSCookieBannerFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<GDSCookieBannerComponent> mapper) : base(documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        _mapper = mapper;
    }

    public override List<GDSCookieBannerComponent> GetMany(QueryRequestArgs? request = null)
        => DocumentQueryClient.QueryMany(
                args: MergeRequest(request, new CssSelector(".govuk-cookie-banner")),
                mapper: _mapper.Map)
            .ToList();
}
