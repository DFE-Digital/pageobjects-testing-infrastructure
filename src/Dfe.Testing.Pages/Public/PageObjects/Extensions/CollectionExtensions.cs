using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.Testing.Pages.Public.PageObjects.Extensions;
public static class CollectionExtensions
{
    public static TOut? TryGetOrDefault<TIn, TOut>(this IEnumerable<KeyValuePair<TIn, TOut>> input, TIn key) where TIn : notnull
    {
        input.ToDictionary(t => t.Key, t => t.Value).TryGetValue(key, out var value);
        return value ?? default;
    }

    public static TOut? TryGetOrDefault<TIn, TOut>(this IDictionary<TIn, TOut> input, TIn key) where TIn : notnull
    {
        input.TryGetValue(key, out var value);
        return value ?? default;
    }
}

