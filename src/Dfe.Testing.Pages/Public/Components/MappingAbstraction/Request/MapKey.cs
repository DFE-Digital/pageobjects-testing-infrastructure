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

    public bool IsEmpty() => keys.Count == 0;

    public override string ToString() => keys.Count switch
    {
        0 => string.Empty,
        1 => keys[0],
        _ => keys.Skip(1).Aggregate(keys[0], (acc, next) => acc + "." + next)
    };
}
