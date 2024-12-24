﻿namespace Dfe.Testing.Pages.Public.Components.GDS.Select;
public record OptionComponent
{
    public required string Value { get; init; }
    public required string Text { get; init; }
    public bool IsSelected { get; init; } = false;
}
