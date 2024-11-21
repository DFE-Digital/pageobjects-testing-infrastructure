﻿namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components.AnchorLink;
public record AnchorLink : IComponent
{
    public string TagName { get; } = "a";
    public required string LinkValue { get; init; }
    public required string Text { get; init; }
    public bool OpensInNewTab { get; init; } = false;
}
