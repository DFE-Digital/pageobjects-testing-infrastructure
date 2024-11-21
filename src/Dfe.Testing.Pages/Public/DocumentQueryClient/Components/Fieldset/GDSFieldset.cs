﻿namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components.Fieldset;

public record GDSFieldset : IComponent
{
    public required string TagName { get; init; }
    public required string Legend { get; init; }
    public required IEnumerable<GDSCheckboxWithLabel> Checkboxes { get; init; }
}
