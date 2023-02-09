using System;
using System.IO;
using OfficeOpenXml;

namespace NetExcel.Services;

public abstract class ExcelBaseService : IDisposable
{
    protected ExcelPackage Doc { get; }

    protected ExcelBaseService(Stream source)
    {
        Doc = new ExcelPackage(source);
    }

    public void Dispose()
    {
        Doc?.Dispose();
    }
}
