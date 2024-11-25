using Dfe.Testing.Pages.Components.Form;

namespace Dfe.Testing.Pages.Internal.ComponentFactory.Form;

internal sealed class FormFactory : ComponentFactory<FormComponent>
{
    // may not be appropriate if there are multiple forms on the page
    internal static IElementSelector DefaultFormQuery = new CssSelector("form");
    private readonly IComponentMapper<FormComponent> _mapper;

    public FormFactory(
        IComponentSelectorFactory componentSelectorFactory,
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<FormComponent> mapper) : base(componentSelectorFactory, documentQueryClientAccessor, mapper)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        _mapper = mapper;
    }

    public override List<FormComponent> GetMany(QueryRequestArgs? request = null)
    {
        var queryRequest = MergeRequest(request, DefaultFormQuery);

        return DocumentQueryClient.QueryMany(
            queryRequest, mapper: _mapper.Map)
                .ToList();
    }
}
