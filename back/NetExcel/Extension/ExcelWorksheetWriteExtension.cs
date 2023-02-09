using System.Collections.Generic;
using OfficeOpenXml;

namespace NetExcel.Extension;

internal static class ExcelWorksheetWriteExtension
{
    internal static void WriteHorizontal<T>(this ExcelWorksheet sheet, IEnumerable<IEnumerable<T>> lst)
    {
        lst.Enumerate((t, x) => sheet.WriteRow(t + 1, x));
    }

    internal static void WriteRow<T>(this ExcelWorksheet sheet, int row, IEnumerable<T> lst)
    {
        lst.Enumerate((t, x) => sheet.Cells[row, t + 1].Set(x));
    }

    internal static void WriteVertical<T>(this ExcelWorksheet sheet, IEnumerable<IEnumerable<T>> lst)
    {
        lst.Enumerate((t, x) => sheet.WriteCol(t + 1, x));
    }

    internal static void WriteCol<T>(this ExcelWorksheet sheet, int col, IEnumerable<T> lst)
    {
        lst.Enumerate((t, x) => sheet.Cells[t + 1, col].Set(x));
    }
}
