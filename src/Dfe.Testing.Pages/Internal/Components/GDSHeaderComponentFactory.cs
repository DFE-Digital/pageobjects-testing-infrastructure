using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.Components;
internal sealed class GDSHeaderComponentFactory : ComponentFactory<GDSHeader>
{
    private readonly ComponentFactory<AnchorLinkComponent> _linkFactory;
    public GDSHeaderComponentFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        ComponentFactory<AnchorLinkComponent> linkFactory) : base(documentQueryClientAccessor)
    {
        _linkFactory = linkFactory;
    }

    private Func<IDocumentPart, GDSHeader> Map => (part) =>
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
        var queryRequest = MergeRequest(request, new CssSelector(".govuk-header"));
        return DocumentQueryClient.QueryMany(queryRequest, Map)
            .ToList();
    }


}
