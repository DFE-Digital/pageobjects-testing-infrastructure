using Dfe.Testing.Pages.Public.DocumentQueryClient.Components;

namespace Dfe.Testing.Pages.Internal.Components.Checkbox;

internal sealed class GDSCheckboxWithLabelFactory : ComponentFactory<GDSCheckboxWithLabelComponent>
{
    private readonly IComponentMapper<GDSCheckboxWithLabelComponent> _mapper;

    internal static IElementSelector GDSCheckboxItemStyle => new CssSelector(".govuk-checkboxes__item");


    public GDSCheckboxWithLabelFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<GDSCheckboxWithLabelComponent> mapper
        ) : base(documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        _mapper = mapper;
    }

    // TODO Checkbox component
    // TODO label component

    public override List<GDSCheckboxWithLabelComponent> GetMany(QueryRequestArgs? request = null)
    {
        var queryRequest = MergeRequest(request, GDSCheckboxItemStyle);

        return DocumentQueryClient.QueryMany(
                args: queryRequest,
                mapper: _mapper.Map)
            .ToList();
    }

    internal List<GDSCheckboxWithLabelComponent> GetCheckboxesFromPart(IDocumentPart? part)
        => part?
            .GetChildren(GDSCheckboxItemStyle)?
            .Select(_mapper.Map)
            .ToList() ?? throw new ArgumentNullException(nameof(part));
}
