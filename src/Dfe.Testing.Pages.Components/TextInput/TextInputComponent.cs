﻿namespace Dfe.Testing.Pages.Components.TextInput;
public record TextInputComponent
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required string? PlaceHolder { get; init; } = null;
    public required string? Type { get; init; } = null;
}
