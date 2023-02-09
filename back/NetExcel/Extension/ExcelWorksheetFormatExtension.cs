using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace NetExcel.Extension;

internal static class ExcelWorksheetFormatExtension
{
    internal static void SetWidth(this ExcelWorksheet sheet, int width)
    {
        for (var x = 1; x <= sheet.Dimension.Columns; ++x)
            sheet.Column(x).Width = width;
    }

    internal static void SetHeight(this ExcelWorksheet sheet, int height)
    {
        for (var x = 1; x <= sheet.Dimension.Rows; ++x)
            sheet.Row(x).Height = height;
    }

    internal static void SetWidth(this ExcelWorksheet sheet, int width, int skip)
    {
        for (var x = 1; x <= sheet.Dimension.Columns; x += skip)
            sheet.Column(x).Width = width;
    }

    internal static void SetHeight(this ExcelWorksheet sheet, int height, int skip)
    {
        for (var x = 1; x <= sheet.Dimension.Rows; x += skip)
            sheet.Row(x).Height = height;
    }

    internal static void SetWidth(this ExcelWorksheet sheet, int width, int skip, int start)
    {
        for (var x = start; x <= sheet.Dimension.Columns; x += skip)
            sheet.Column(x).Width = width;
    }

    internal static void SetHeight(this ExcelWorksheet sheet, int height, int skip, int start)
    {
        for (var x = start; x <= sheet.Dimension.Rows; x += skip)
            sheet.Row(x).Height = height;
    }

    internal static void SetRowVerticalAlignment(this ExcelWorksheet sheet, ExcelVerticalAlignment align)
    {
        for (var x = 1; x <= sheet.Dimension.Rows; ++x)
            sheet.Row(x).Style.VerticalAlignment = align;
    }

    internal static void SetRowHorizontalAlignment(this ExcelWorksheet sheet, ExcelHorizontalAlignment align)
    {
        for (var x = 1; x <= sheet.Dimension.Rows; ++x)
            sheet.Row(x).Style.HorizontalAlignment = align;
    }

    internal static void SetRowVerticalAlignment(this ExcelWorksheet sheet, ExcelVerticalAlignment align, int row)
    {
        sheet.Row(row).Style.VerticalAlignment = align;
    }

    internal static void SetRowHorizontalAlignment(this ExcelWorksheet sheet, ExcelHorizontalAlignment align, int row)
    {
        sheet.Row(row).Style.HorizontalAlignment = align;
    }
}
