using System.Linq;
using Common.Enums;
using Common.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace API.Infrastructure;

internal sealed class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params RoleType[] allowedRoles)
    {
        Roles = string.Join(",", allowedRoles.Select(x => x.Description()));
    }
}
