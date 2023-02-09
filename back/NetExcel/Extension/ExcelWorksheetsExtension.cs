using System;
using System.Linq;
using OfficeOpenXml;

namespace NetExcel.Extension;

internal static class ExcelWorksheetsExtension
{
    internal static ExcelWorksheet ByName(this ExcelWorksheets sheets, string name)
    {
        var sheet = sheets.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase));
        if (sheet.IsNull())
            throw new ArgumentException($"2592. Sheet with name '{name}' wasn't found");

        return sheet;
    }

    internal static ExcelWorksheet FirstOrThrow(this ExcelWorksheets sheets)
    {
        var sheet = sheets.FirstOrDefault();
        if (sheet.IsNull())
            throw new ArgumentException($"2593. Doesn't contain no one sheet");

        return sheet;
    }
}
