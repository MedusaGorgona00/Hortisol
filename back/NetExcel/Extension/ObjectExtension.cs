namespace NetExcel.Extension;

internal static class ObjectExtension
{
    internal static bool IsNotNull(this object x)
    {
        return !x.IsNull();
    }

    internal static bool IsNull(this object x)
    {
        return x == null;
    }
}
