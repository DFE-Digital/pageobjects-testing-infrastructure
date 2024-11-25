using Dfe.Testing.Pages.Components.TextInput;

namespace Dfe.Testing.Pages.Internal.ComponentFactory.TextInput;
internal sealed class GDSTextInputFactory : ComponentFactory<GDSTextInputComponent>
{
    private static readonly CssSelector _defaultContainerQuery = new(".govuk-form-group:has(input[type=text])");
    private readonly IComponentMapper<GDSTextInputComponent> _mapper;

    public GDSTextInputFactory(IComponentSelectorFactory componentSelectorFactory,
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<GDSTextInputComponent> mapper) : base(componentSelectorFactory, documentQueryClientAccessor, mapper)
    {
        _mapper = mapper;
    }

    public override List<GDSTextInputComponent> GetMany(QueryRequestArgs? request = null)
    {
        return DocumentQueryClient.QueryMany(
            args: MergeRequest(request, _defaultContainerQuery),
            mapper: _mapper.Map)
                .ToList();
    }
}
