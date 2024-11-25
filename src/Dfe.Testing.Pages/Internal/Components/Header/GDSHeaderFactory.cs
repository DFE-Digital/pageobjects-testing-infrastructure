using Dfe.Testing.Pages.Public.DocumentQueryClient.Components;

namespace Dfe.Testing.Pages.Internal.Components.Header;
internal sealed class GDSHeaderFactory : ComponentFactory<GDSHeaderComponent>
{
    private readonly IComponentMapper<GDSHeaderComponent> _mapper;
    public GDSHeaderFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<GDSHeaderComponent> mapper) : base(documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        _mapper = mapper;
    }

    public override List<GDSHeaderComponent> GetMany(QueryRequestArgs? request = null)
    {
        return DocumentQueryClient.QueryMany(
                args: MergeRequest(request, new CssSelector(".govuk-header")),
                mapper: _mapper.Map)
            .ToList();
    }
}
