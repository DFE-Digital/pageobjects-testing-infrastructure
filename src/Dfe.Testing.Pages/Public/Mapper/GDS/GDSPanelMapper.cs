﻿using Dfe.Testing.Pages.Components.Panel;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSPanelMapper : IComponentMapper<GDSPanelComponent>
{
    public GDSPanelComponent Map(IDocumentPart input)
    {
        return new()
        {
            Heading = input.GetChild(new CssElementSelector(".govuk-panel__title"))?.Text ?? throw new ArgumentNullException("panel has no heading"),
            Content = input.GetChild(new CssElementSelector(".govuk-panel__body"))?.Text ?? throw new ArgumentNullException("panel has no content")
        };
    }
}
