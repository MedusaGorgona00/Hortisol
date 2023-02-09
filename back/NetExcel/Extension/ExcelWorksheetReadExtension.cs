using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;

namespace NetExcel.Extension;

internal static class ExcelWorksheetReadExtension
{
    internal static IEnumerable<(string Name, IList<IList<T>> Sheet)> Read<T>(this ExcelWorksheets sheets, Func<object, T> convert)
    {
        return sheets
            .Select(x => (x.Name, x.Read(convert)))
            .ToList();
    }

    internal static IList<IList<T>> Read<T>(this ExcelWorksheet sheet, Func<object, T> convert)
    {
        var lst = new List<IList<T>>();
        for (var x = 1; x <= sheet?.Dimension.Rows; ++x)
            lst.Add(sheet.Read(x, convert));

        return lst;
    }

    internal static IList<T> Read<T>(this ExcelWorksheet sheet, int row, Func<object, T> convert)
    {
        var lst = new List<T>();
        for (var x = 1; x <= sheet?.Dimension?.Columns; ++x)
            lst.Add(convert(sheet.Cells[row, x].Value));

        return lst;
    }
}
