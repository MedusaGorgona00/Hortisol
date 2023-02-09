using System.Collections.Generic;
using Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities.Users;

public class User : IdentityUser, IIdHas<string>
{
    public ICollection<UserRole> Roles { get; set; }
}
