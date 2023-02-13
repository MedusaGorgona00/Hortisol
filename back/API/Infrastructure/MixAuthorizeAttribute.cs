using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Enums;
using Common.Extensions;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace API.Infrastructure;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal sealed class MixAuthorizeAttribute : Attribute, IAsyncActionFilter
{
    private string HeaderName { get; }

    private IEnumerable<string> Roles { get; }

    internal MixAuthorizeAttribute(params RoleType[] allowedRoles)
    {
        HeaderName = JwtBuilder.Authorization();
        Roles = allowedRoles.Select(x => x.Description());
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var unauthorized = new UnauthorizedResult();
        var (token, isSecret, isBearer) = IsCorrect(context);
        if (!(isSecret ^ isBearer))
        {
            context.Result = unauthorized;
            return;
        }

        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        if (isBearer)
        {
            var (isCorrectBearer, result) = IsCorrectBearer(context, configuration, token);
            if (!isCorrectBearer)
            {
                context.Result = result;
                return;
            }
        }

        await next();
    }

    private (string Token, bool IsSecret, bool IsBearer) IsCorrect(ActionExecutingContext context)
    {
        if (context.ActionDescriptor is not ControllerActionDescriptor actionDescriptor)
            return (null, false, false);

        var isHasAuthorizeHeader = context.HttpContext.Request.Headers.TryGetValue(HeaderName, out var headerKeys);

        if (!actionDescriptor.MethodInfo.IsDefined(typeof(AuthorizeAttribute), true) && !isHasAuthorizeHeader)
            return (null, false, false);

        var keys = headerKeys.FirstOrDefault()?.Split(" ");
        var last = keys?.Last();
        var isBearer = keys?.Length == 2 && keys.FirstOrDefault()?.ToLower() == JwtBuilder.Bearer() && last.IsNotNullOrWhiteSpace();
        var isSecret = keys?.Length == 1 && last.IsNotNullOrWhiteSpace();

        return (last, isSecret, isBearer);
    }
    
    private (bool IsCorrect, IActionResult Result) IsCorrectBearer(ActionExecutingContext context, IConfiguration configuration, string accessToken)
    {
        var token = new JsonWebTokenHandler().ValidateToken(accessToken, JwtBuilder.Parameters(configuration));
        var roleNames = new HashSet<string>(token?.ClaimsIdentity?.Claims
            .Where(x => x.Type == ClaimTypes.Role)
            .Select(x => x.Value) ?? new List<string>());

        IActionResult result = roleNames.Any() ? new ForbidResult() : new UnauthorizedResult();

        return (Roles.Any(x => roleNames.Contains(x)), result);
    }
}
