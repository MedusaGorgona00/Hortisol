using System.ComponentModel;

namespace Common.Enums;

public enum FilterType : byte
{
    [Description("Total")]
    Total = 1,

    [Description("By grade")]
    ByGrade = 2,

    [Description("By company")]
    ByCompany = 3,

    [Description("Type-A, Small package")]
    SmallPackage = 4,

    [Description("Type-B, Large package")]
    LargePackage = 5,
}