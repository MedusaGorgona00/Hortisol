using System.ComponentModel.DataAnnotations;
using BLL.Services.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Common.Enums;

namespace API.Controllers;

[Route("api/Reports")]
[ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
public class ReportsController : BaseController
{
    private ReportDtoService Service { get; }

    public ReportsController(ReportDtoService service)
    {
        Service = service;
    }
    
    /// <summary>
    /// Get report
    /// </summary>
    /// <response code="500">Unknown error</response>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
    public string List([Required] StateType state, [FromQuery] FiltrationDto filter)
    {
        return Service.List(state, filter);
    }
}
