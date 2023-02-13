using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Tables;

public class ReportService
{
    private AppDbContext Context { get; }

    public ReportService(AppDbContext context)
    {
        Context = context;
    }

    public IQueryable<T> List<T>(FormattableString sql) where T : class
    {
        return Context.Set<T>()
            .FromSql(sql)
            .AsNoTracking();
    }
}

