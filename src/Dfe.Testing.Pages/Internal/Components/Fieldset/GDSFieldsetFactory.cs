using Dfe.Testing.Pages.Internal.Components.Checkbox;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.Components.Fieldset;

internal sealed class GDSFieldsetFactory : ComponentFactory<GDSFieldsetComponent>
{
    private readonly IComponentMapper<GDSFieldsetComponent> _mapper;

    public GDSFieldsetFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<GDSFieldsetComponent> mapper
        ) : base(documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        _mapper = mapper;
    }

    public override List<GDSFieldsetComponent> GetMany(QueryRequestArgs? request = null)
    {
        var queryRequest = MergeRequest(request, new CssSelector("fieldset"));

        return DocumentQueryClient.QueryMany(
            queryRequest,
            mapper: _mapper.Map).ToList();
    }
}
