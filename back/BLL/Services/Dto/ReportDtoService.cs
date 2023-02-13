using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BLL.Services.Tables;
using Common.Enums;
using Common.Exceptions;
using Common.Extensions;
using Common.Resources;
using DTO;
using DTO.States;

namespace BLL.Services.Dto;

public sealed class ReportDtoService
{
    #region Properties
    private ReportService Service { get; }
    #endregion

    public ReportDtoService(ReportService service)
    {
        Service = service;
    }

    public IEnumerable<T> List<T>(FormattableString sql) where T : class
    {
        return Service.List<T>(sql);
    }

    public string List(StateType state, FiltrationDto filter)
    {
        var script = BuildSqlScript(state, filter);
        var errorMsg = $"State {state.Name()} doesn't have such report type - {filter.Type.Description()}";

        var result = state switch
        {
            StateType.Washington => filter.Type switch
            {
                FilterType.Total => Service.List<WashingtonDto.Total>(script).ToJson(),
                FilterType.ByGrade => Service.List<WashingtonDto.ByGrade>(script).ToJson(),
                FilterType.ByCompany => Service.List<WashingtonDto.ByCompany>(script).ToJson(),
                _ => throw new InnerException(errorMsg),
            },
            StateType.Illinois => filter.Type switch
            {
                FilterType.SmallPackage => Service.List<IllinoisDto.SmallPackage>(script).ToJson(),
                FilterType.LargePackage => Service.List<IllinoisDto.LargePackage>(script).ToJson(),
                _ => throw new InnerException(errorMsg),
            },
            _ => string.Empty
        };
        
        return result;
    }

    private FormattableString BuildSqlScript(StateType state, FiltrationDto filter)
    {
        var startDate = $"'{filter.StartDate ?? DateTime.MinValue.Date:yyyy-MM-dd}'";
        var endDate = $"'{filter.EndDate ?? DateTime.MaxValue.Date:yyyy-MM-dd}'";
        var scriptName = string.Concat(state, filter.Type);
        var scriptValue = Scripts.ResourceManager
            .GetString(scriptName);

        if (scriptValue.IsNullOrEmpty())
            throw new InnerException("There is no script for this state and report type");
        
        return FormattableStringFactory.Create($"{string.Format(scriptValue, startDate, endDate)}");
    }
}
