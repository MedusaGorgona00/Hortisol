using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BLL.Services.Tables;
using Common.Enums;
using Common.Exceptions;
using Common.Extensions;
using Common.Helpers;
using Common.Resources;
using DTO;
using DTO.States;
using NetExcel.Services;

namespace BLL.Services.Dto;

public sealed class ReportDtoService
{
    #region Properties
    private ReportService Service { get; }
    private ExcelWriteService ExcelService { get; }
    #endregion

    public ReportDtoService(ReportService service)
    {
        Service = service;
    } 

    public async Task<string> Report(StateType state, FiltrationDto filter, SortingDto sort, bool download = false)
    {
        if (!AppConstants.AllowedFilters[state].Contains(filter.FilterType))
            throw new InnerException($"1501. Allowed filters for { state.Name()} state - {AppConstants.AllowedFilters[state].Select(x => x.Name()).Join()}");

        var script = BuildSqlScript(state, filter, sort);
        var result = await (state switch
        {
            StateType.Washington => filter.FilterType switch
            {
                FilterType.Total => Report<WashingtonDto.Total>(script, download),
                FilterType.ByGrade => Report<WashingtonDto.ByGrade>(script, download),
                FilterType.ByCompany => Report<WashingtonDto.ByCompany>(script, download),
            },
            StateType.Illinois => filter.FilterType switch
            {
                FilterType.SmallPackage => Report<IllinoisDto.SmallPackage>(script, download),
                FilterType.LargePackage => Report<IllinoisDto.LargePackage>(script, download),
            },
            _ => throw new InnerException($"1502. State doesn't exist"),
        });
        
        return result;
    }

    private FormattableString BuildSqlScript(StateType state, FiltrationDto filter, SortingDto sort)
    {
        var startDate = $"'{filter.StartDate ?? DateTime.MinValue.Date:yyyy-MM-dd}'";
        var endDate = $"'{filter.EndDate ?? DateTime.MaxValue.Date:yyyy-MM-dd}'";
        var scriptName = string.Concat(state, filter.FilterType);
        var scriptValue = Scripts.ResourceManager.GetString(scriptName);
        var field = sort.Field?.Capitalize();
        var order = field.IsNull() || sort.SortType.IsNull()
            ? AppConstants.DefaultSort[state][filter.FilterType]
            : $"order by \"{field}\" {sort.SortType?.Name()}";

        if (field.IsNotNull() && !AppConstants.AllowedSort[state][filter.FilterType].Contains(field))
            throw new InnerException($"1504. Allowed sorting fields for {filter.FilterType.Name()} filter - {AppConstants.AllowedSort[state][filter.FilterType].Join()}");

        if (scriptValue.IsNullOrEmpty()) 
            throw new InnerException("1503. There is no script for this state and report type");
        
        return FormattableStringFactory.Create($"{string.Format(scriptValue, startDate, endDate, order)}");
    }

    private async Task<string> Report<T>(FormattableString script, bool download = false) where T : class
    {
        if (download)
            return await Download<T>(script);

        return Service.List<T>(script).ToJson();
    }

    private async Task<string> Download<T>(FormattableString script) where T : class
    {
        var items = Service.List<T>(script)
            .ToList();
        var type = items
            .FirstOrDefault()?
            .GetType();
        var properties = type?
            .GetProperties();
        var lst = items
            .Select(item => properties?
                .Select(x => GetPropValue(item, x.Name)))
            .ToList();
        lst.Insert(0, properties?.Select(x => x.Name));

        var source = new MemoryStream();
        var excelService = new ExcelWriteService(source);
        excelService.WriteHorizontal(nameof(Report), lst);
        excelService.Save();

        return await TempLinkService.Create((source, $"{type?.DeclaringType?.Name}{type?.Name}.xlsx"));
    }

    public static string GetPropValue(object src, string propName)
        => src.GetType().GetProperty(propName)?.GetValue(src, null)?.ToString();
}
