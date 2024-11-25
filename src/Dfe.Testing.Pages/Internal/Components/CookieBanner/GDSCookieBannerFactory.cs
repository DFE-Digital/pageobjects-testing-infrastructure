using Dfe.Testing.Pages.Public.DocumentQueryClient.Components;

namespace Dfe.Testing.Pages.Internal.Components.CookieBanner;
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
