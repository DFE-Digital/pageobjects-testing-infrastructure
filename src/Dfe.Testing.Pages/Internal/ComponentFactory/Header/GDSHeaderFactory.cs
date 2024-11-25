﻿namespace Dfe.Testing.Pages.Internal.ComponentFactory.Header;
internal sealed class GDSHeaderFactory : ComponentFactory<GDSHeaderComponent>
{
    private readonly IComponentMapper<GDSHeaderComponent> _mapper;
    public GDSHeaderFactory(IComponentSelectorFactory componentSelectorFactory,
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<GDSHeaderComponent> mapper) : base(componentSelectorFactory, documentQueryClientAccessor, mapper)
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
