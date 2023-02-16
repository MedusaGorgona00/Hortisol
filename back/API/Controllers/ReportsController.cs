using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BLL.Services.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Common.Enums;

namespace API.Controllers;

[Route("api/Reports")]
public class ReportsController : BaseController
{
    private ReportDtoService Service { get; }

    public ReportsController(ReportDtoService service)
    {
        Service = service;
    }

    /// <summary>
    /// Get report by filter
    /// </summary>
    /// <param name="state">state</param>
    /// <param name="filter">filtration model</param>
    /// <param name="sort">sorting model</param>
    /// <response code="500">Unknown error</response>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
    public async Task<string> List([Required] StateType state, [FromQuery] FiltrationDto filter, [FromQuery] SortingDto sort)
    {
        return await Service.Report(state, filter, sort);
    }

    /// <summary>
    /// Download report by filter
    /// </summary>
    /// <param name="state">state</param>
    /// <param name="filter">filtration model</param>
    /// <param name="sort">sorting model</param>
    /// <response code="500">Unknown error</response>
    [HttpPost]
    [Route("Download")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
    public async Task<string> Download([Required] StateType state, [FromQuery] FiltrationDto filter, [FromQuery] SortingDto sort)
    {
        return await Service.Report(state, filter, sort, download: true);
    } 
}
