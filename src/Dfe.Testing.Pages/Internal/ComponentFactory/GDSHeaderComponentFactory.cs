using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.ComponentFactory;
internal sealed class GDSHeaderComponentFactory : ComponentFactory<GDSHeader>
{
    private readonly ComponentFactory<AnchorLink> _linkFactory;
    public GDSHeaderComponentFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        ComponentFactory<AnchorLink> linkFactory) : base(documentQueryClientAccessor)
    {
        _linkFactory = linkFactory;
    }

    private Func<IDocumentPart, GDSHeader> Map => (IDocumentPart part) =>
    {
        return new GDSHeader()
        {
            GovUKLink = _linkFactory.Get(new QueryRequestArgs()
            {
                Query = new CssSelector(".govuk-header__link--homepage"),
            }),
            ServiceName = _linkFactory.Get(new QueryRequestArgs()
            {
                Query = new CssSelector(".govuk-header__service-name"),
            }),
            TagName = part.TagName
        };
    };

    public override List<GDSHeader> GetMany(QueryRequestArgs? request = null)
    {
        QueryRequestArgs queryRequest = MergeRequest(request, new CssSelector(".govuk-header"));
        return DocumentQueryClient.QueryMany<GDSHeader>(queryRequest, Map)
            .ToList();
    }


}
