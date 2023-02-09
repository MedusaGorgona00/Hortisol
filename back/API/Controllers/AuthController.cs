using System.Threading.Tasks;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;

namespace API.Controllers;

[Route("api/Token")]
[ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
public class AuthController : BaseController
{
    private AuthService Service { get; }

    public AuthController(AuthService service)
    {
        Service = service;
    }

    /// <summary>
    /// Get token by login and password
    /// </summary>
    /// <param name="model">Login and password</param>
    /// <response code="500">Unknown error</response>
    [HttpPost]
    [Route("Access")]
    [ProducesResponseType(typeof(AuthDto.Response), StatusCodes.Status200OK)]
    public async Task<AuthDto.Response> AccessToken(AuthDto.Login model)
    {
        return await Service.AccessToken(model);
    }

    /// <summary>
    /// Get access token by refresh token
    /// </summary>
    /// <param name="model">Refresh token</param>
    /// <response code="500">Unknown error</response>
    [HttpPost]
    [Route("Refresh")]
    [ProducesResponseType(typeof(AuthDto.Response), StatusCodes.Status200OK)]
    public async Task<AuthDto.Response> RefreshToken(AuthDto.Refresh model)
    {
        return await Service.RefreshToken(model);
    }
}
