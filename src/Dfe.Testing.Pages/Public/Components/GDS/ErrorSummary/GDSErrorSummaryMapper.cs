﻿using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorSummary;
internal sealed class GDSErrorSummaryMapper : IComponentMapper<GDSErrorSummaryComponent>
{
    private readonly ComponentFactory<AnchorLinkComponent> _anchorLinkFactory;

    public GDSErrorSummaryMapper(ComponentFactory<AnchorLinkComponent> anchorLinkFactory)
    {
        ArgumentNullException.ThrowIfNull(anchorLinkFactory);
        _anchorLinkFactory = anchorLinkFactory;
    }
    public GDSErrorSummaryComponent Map(IDocumentPart input)
    {
        return new()
        {
            Heading = input.FindDescendant(new CssElementSelector(".govuk-error-summary__title"))?.Text ?? throw new ArgumentNullException("heading on error summary is null"),
            Errors = _anchorLinkFactory.GetManyFromPart(input)
        };
    }
}
