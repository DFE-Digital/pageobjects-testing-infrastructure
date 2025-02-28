﻿using Dfe.Testing.Pages.Public.PageObjects.Extensions;

namespace Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Response;
public record CreatedPageObjectModel
{
    public string Id { get; init; } = string.Empty;
    public required PageObjectSchemaResponse Status { get; init; } // query failed, mapping failed, success
    public IDictionary<string, string?> Results { get; init; } = new Dictionary<string, string?>();
    public IList<CreatedPageObjectModel> Children { get; init; } = [];
    public string? GetAttribute(string attribute) => Results.TryGetOrDefault(attribute);
}
