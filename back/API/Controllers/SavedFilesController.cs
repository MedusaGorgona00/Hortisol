using System.Collections.Generic;
using System.Threading.Tasks;
using API.Extensions;
using API.Infrastructure;
using BLL.Services.Dto;
using Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;

namespace API.Controllers;

[Route("api/SavedFiles")]
[AuthorizeRoles(RoleType.Admin, RoleType.Operator)]
[ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
public class SavedFilesController : BaseController
{
    private SavedFileDtoService Service { get; }

    public SavedFilesController(SavedFileDtoService service)
    {
        Service = service;
    }

    /// <summary>
    /// Получить список
    /// </summary>
    /// <response code="401">Требуется авторизация</response>
    /// <response code="500">Неотловленная, неизвестная ошибка</response>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IEnumerable<SavedFileDto.ListIdOut>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
    public async Task<IEnumerable<SavedFileDto.ListIdOut>> List()
    {
        return await Service.List();
    }

    /// <summary>
    /// Получить по коду
    /// </summary>
    /// <param name="id">Код</param>
    /// <response code="400">По коду не найдено</response>
    /// <response code="401">Требуется авторизация</response>
    /// <response code="500">Неотловленная, неизвестная ошибка</response>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(SavedFileDto.ByIdOut), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
    public async Task<SavedFileDto.ByIdOut> ById(int id)
    {
        return await Service.ById(id);
    }

    /// <summary>
    /// Редактировать
    /// </summary>
    /// <param name="dto">Данные для редактировия</param>
    /// <response code="400">Ошибка в данных модели</response>
    /// <response code="401">Требуется авторизация</response>
    /// <response code="500">Неотловленная, неизвестная ошибка</response>
    [HttpPut]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
    public async Task Edit([FromForm] SavedFileDto.EditIn<IFormFile> dto)
    {
        await Service.Edit(dto.Id, await dto.File.ToStream());
    }

    /// <summary>
    /// Удалить
    /// </summary>
    /// <param name="id">Код</param>
    /// <response code="400">По коду не найдено</response>
    /// <response code="401">Требуется авторизация</response>
    /// <response code="500">Неотловленная, неизвестная ошибка</response>
    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
    public async Task Delete(int id)
    {
        await Service.Delete(id);
    }
}
