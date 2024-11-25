﻿using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.Components.Button;

internal sealed class GDSButtonFactory : ComponentFactory<GDSButtonComponent>
{
    private readonly IComponentMapper<GDSButtonComponent> _mapper;

    internal static IElementSelector DefaultButtonStyleSelector => new CssSelector(".govuk-button");

    public GDSButtonFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<GDSButtonComponent> mapper) : base(documentQueryClientAccessor)
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