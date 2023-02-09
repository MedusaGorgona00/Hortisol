using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NetExcel.Extension;

namespace NetExcel.Services;

public sealed class ExcelReadService : ExcelBaseService
{
    public ExcelReadService(Stream source) : base(source) { }

    public IEnumerable<string> SheetNames()
    {
        return Doc.Workbook.Worksheets.Select(x => x.Name);
    }

    public IEnumerable<(string Name, IList<IList<T>> Sheet)> Read<T>(Func<object, T> convert)
    {
        return Doc.Workbook.Worksheets.Read(convert);
    }

    public IList<IList<T>> Read<T>(string name, Func<object, T> convert)
    {
        var sheet = Doc.Workbook.Worksheets.ByName(name);
        return sheet.Read(convert);
    }
}
