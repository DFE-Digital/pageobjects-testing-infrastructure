﻿namespace Dfe.Testing.Pages.Public.Components.GDS.Panel;
public record GDSPanelComponent : IComponent
{
    public required string Heading { get; init; }
    public required string Content { get; init; }
}
