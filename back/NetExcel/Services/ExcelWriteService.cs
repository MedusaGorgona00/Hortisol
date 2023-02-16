using System;
using System.Collections.Generic;
using System.IO;
using NetExcel.Extension;
using OfficeOpenXml;

namespace NetExcel.Services;

public sealed class ExcelWriteService : ExcelBaseService
{
    public ExcelWriteService(Stream source) : base(source) { }

    public void Save()
    {
        Doc.Save();
    }

    public void WriteHorizontal<T>(string name, ICollection<IEnumerable<T>> lst)
    {
        Check(name, lst);
        var sheet = Doc?.Workbook.Worksheets.Add(name);
        sheet?.WriteHorizontal(lst);
        sheet?.Cells[sheet.Dimension.Address].AutoFitColumns();
    }

    public void WriteVertical<T>(string name, ICollection<IEnumerable<T>> lst)
        where T : struct
    {
        Check(name, lst);
        var sheet = Doc?.Workbook.Worksheets.Add(name);
        sheet?.WriteVertical(lst);
        sheet?.Cells[sheet.Dimension.Address].AutoFitColumns();
    }

    private void Check<T>(string name, ICollection<IEnumerable<T>> lst)
    {
        if (name.IsNullOrEmpty())
            throw new ArgumentException($"{nameof(name)} is can't be null or empty");

        if (lst.IsNullOrEmpty())
            throw new ArgumentException($"{nameof(lst)} should be filled");
    }
}
