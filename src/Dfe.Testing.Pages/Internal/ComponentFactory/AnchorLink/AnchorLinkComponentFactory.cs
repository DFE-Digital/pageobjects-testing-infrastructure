namespace Dfe.Testing.Pages.Internal.ComponentFactory.AnchorLink;
internal sealed class AnchorLinkComponentFactory : ComponentFactory<AnchorLinkComponent>
{
    private readonly IComponentMapper<AnchorLinkComponent> _mapper;

    public AnchorLinkComponentFactory(
        IComponentSelectorFactory componentSelectorFactory,
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<AnchorLinkComponent> mapper) : base(componentSelectorFactory, documentQueryClientAccessor, mapper)
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
