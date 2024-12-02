﻿namespace Dfe.Testing.Pages.Components.Inputs.TextInput;
public record TextInputComponent : IComponent
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public string Type { get; init; } = "text";
    public bool IsNumeric { get; init; } = false;
    public string AutoComplete { get; init; } = string.Empty;
    public string? PlaceHolder { get; init; } = string.Empty;
    public string Hint { get; init; } = string.Empty;
    public bool IsRequired { get; init; } = false;
}
