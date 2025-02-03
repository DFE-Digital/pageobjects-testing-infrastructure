namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
public sealed class MapKey
{
    private readonly List<string> keys = [];
    internal MapKey(IEnumerable<string> mappingKeys)
    {
        keys = mappingKeys.ToList();
    }

    public MapKey Append(string appendKey)
    {
        List<string> copiedKeys = [.. keys];
        if (!string.IsNullOrEmpty(appendKey))
        {
            copiedKeys.Add(appendKey);
        }
        // must copy across so original isn't modified per each MapRequest - retains maplookupkey chain per request
        return new MapKey(copiedKeys);
    }

    public override string ToString()
    {
        return keys.Count == 0 ? string.Empty : keys.Count == 1 ? keys[0] : keys.Skip(1).Aggregate(keys[0], (acc, next) => acc + "." + next);
    }
}
