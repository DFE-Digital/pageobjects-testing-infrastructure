using Dfe.Testing.Pages.Components.Button;

namespace Dfe.Testing.Pages.Internal.ComponentFactory.Button;

internal sealed class GDSButtonFactory : ComponentFactory<GDSButtonComponent>
{
    private readonly IComponentMapper<GDSButtonComponent> _mapper;

    internal static IElementSelector DefaultButtonStyleSelector => new CssSelector(".govuk-button");

    public GDSButtonFactory(
        IComponentSelectorFactory componentSelectorFactory,
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<GDSButtonComponent> mapper) : base(componentSelectorFactory, documentQueryClientAccessor, mapper)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        _mapper = mapper;
    }

    public override List<GDSButtonComponent> GetMany(QueryRequestArgs? request = null)
    {
        var queryRequest = MergeRequest(request, DefaultButtonStyleSelector);

        return DocumentQueryClient.QueryMany(
            args: queryRequest,
            mapper: _mapper.Map).ToList();
    }
}
