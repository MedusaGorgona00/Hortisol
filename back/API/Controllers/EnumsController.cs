using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Common.Enums;
using System.Collections.Generic;
using Common.Extensions;
using DTO.States;

namespace API.Controllers;

[Route("api/Enums")]
[ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
public class EnumsController : BaseController
{
    /// <summary>
    /// get list of states with uniq filters
    /// </summary>
    /// <response code="400">payload error</response>
    /// <response code="401">unauthorized</response>
    /// <response code="403">forbidden</response>
    /// <response code="500">uncaught, unknown error</response>
    [HttpGet]
    [Route("States")]
    [ProducesResponseType(typeof(IEnumerable<BaseStateDto>), StatusCodes.Status200OK)]
    public IEnumerable<BaseStateDto> States() => new BaseStateDto[]
    {
        new WashingtonDto.Self(),
        new IllinoisDto.Self(),
    };

    /// <summary>
    /// get list of state types
    /// </summary>
    /// <response code="400">payload error</response>
    /// <response code="401">unauthorized</response>
    /// <response code="403">forbidden</response>
    /// <response code="500">uncaught, unknown error</response>
    [HttpGet]
    [Route("StateTypes")]
    [ProducesResponseType(typeof(IEnumerable<EnumDto>), StatusCodes.Status200OK)]
    public IEnumerable<EnumDto> StateTypes() => EnumExtension.ToList<StateType, EnumDto>();

    /// <summary>
    /// get list of filter types
    /// </summary>
    /// <response code="400">payload error</response>
    /// <response code="401">unauthorized</response>
    /// <response code="403">forbidden</response>
    /// <response code="500">uncaught, unknown error</response>
    [HttpGet]
    [Route("FilterTypes")]
    [ProducesResponseType(typeof(IEnumerable<EnumDto>), StatusCodes.Status200OK)]
    public IEnumerable<EnumDto> FilterTypes() => EnumExtension.ToList<FilterType, EnumDto>();

    /// <summary>
    /// get list of sorting types
    /// </summary>
    /// <response code="400">payload error</response>
    /// <response code="401">unauthorized</response>
    /// <response code="403">forbidden</response>
    /// <response code="500">uncaught, unknown error</response>
    [HttpGet]
    [Route("SortTypes")]
    [ProducesResponseType(typeof(IEnumerable<EnumDto>), StatusCodes.Status200OK)]
    public IEnumerable<EnumDto> SortingTypes() => EnumExtension.ToList<SortType, EnumDto>();
}