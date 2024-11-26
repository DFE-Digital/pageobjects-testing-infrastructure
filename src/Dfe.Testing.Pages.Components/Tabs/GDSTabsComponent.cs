﻿using Dfe.Testing.Pages.Components.AnchorLink;
using Dfe.Testing.Pages.Public.DocumentQueryClient;

namespace Dfe.Testing.Pages.Components.Tabs;
public record GDSTabsComponent : IComponent
{
    public required IEnumerable<AnchorLinkComponent> Tabs { get; init; }
    public string Heading { get; init; } = string.Empty;
    public string TagName { get; init; } = string.Empty;
}