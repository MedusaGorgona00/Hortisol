using System;
using System.Collections.Generic;
using System.Linq;

namespace NetExcel.Extension;

internal static class IEnumerableExtension
{
    internal static bool IsNullOrEmpty<T>(this IEnumerable<T> lst)
    {
        return lst.IsNull() || !lst.Any();
    }

    internal static void Enumerate<T>(this IEnumerable<T> lst, Action<int, T> act)
    {
        var t = 0;
        foreach (var x in lst)
            act(t++, x);
    }
}
