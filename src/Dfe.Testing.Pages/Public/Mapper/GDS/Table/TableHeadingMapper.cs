﻿using Dfe.Testing.Pages.Components.Table;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS.Table;
internal class TableHeadingMapper : IComponentMapper<TableHeading>
{
    public TableHeading Map(IDocumentPart input)
    {
        return new()
        {
            Scope = input.GetAttribute("scope") ?? string.Empty,
            Text = input.Text ?? string.Empty
        };
    }
}
