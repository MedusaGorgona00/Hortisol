using System.ComponentModel;

namespace Common.Enums;

public enum RoleType
{
    [Description("Admin")]
    Admin = 1,
    
    [Description("Operator")]
    Operator = 2,
}
