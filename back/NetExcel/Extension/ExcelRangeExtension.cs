using System;
using OfficeOpenXml;

namespace NetExcel.Extension;

internal static class ExcelRangeExtension
{
    internal static void Set<T>(this ExcelRange cell, T value)
    {
        if (value.IsNull())
            return;

        switch (value)
        {
            case string x:
                cell.Set(x);
                break;

            case int x:
                cell.SimpleSet(x);
                break;

            case decimal x:
                cell.SimpleSet(x);
                break;

            case bool x:
                cell.SimpleSet(x);
                break;

            case DateTime x:
                cell.Set($"{x:G}");
                break;

            default:
                throw new ArgumentException($"2670. '{typeof(T)}' - неизвестный тип.");
        }
    }

    private static void SimpleSet<T>(this ExcelRange cell, T value)
        where T : struct
    {
        cell.Value = value;
    }

    private static void Set(this ExcelRange cell, string value)
    {
        cell.Value = value;
    }
}
