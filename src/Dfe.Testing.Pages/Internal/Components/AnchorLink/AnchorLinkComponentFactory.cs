using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.Components.AnchorLink;
internal sealed class AnchorLinkComponentFactory : ComponentFactory<AnchorLinkComponent>
{
    private readonly IComponentMapper<AnchorLinkComponent> _mapper;

    public AnchorLinkComponentFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<AnchorLinkComponent> mapper) : base(documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        _mapper = mapper;
    }

    public override List<AnchorLinkComponent> GetMany(QueryRequestArgs? request = null)
         => DocumentQueryClient.QueryMany(
                args: MergeRequest(
                    request,
                    defaultFindBySelector: new CssSelector("a")),
                mapper: _mapper.Map).ToList();
}
