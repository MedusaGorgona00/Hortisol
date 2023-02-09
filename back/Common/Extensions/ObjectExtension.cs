﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Extensions;

public static class ObjectExtension
{
    public static bool IsNotNull(this object x)
    {
        return !x.IsNull();
    }

    public static bool IsNull(this object x)
    {
        return x == null;
    }

    public static string ToJson<T>(this T x)
    {
        return JsonSerializer.Serialize(x);
    }

    public static string ToJson<T>(this T x, ReferenceHandler rh)
    {
        return JsonSerializer.Serialize(x, new JsonSerializerOptions
        {
            ReferenceHandler = rh,
        });
    }

    public static T FromJson<T>(this string x)
        where T : class
    {
        return x.IsNullOrEmpty()
            ? null
            : JsonSerializer.Deserialize<T>(x);
    }

    public static T FromJson<T>(this string x, ReferenceHandler rh)
    {
        return JsonSerializer.Deserialize<T>(x, new JsonSerializerOptions
        {
            ReferenceHandler = rh,
        });
    }
}
