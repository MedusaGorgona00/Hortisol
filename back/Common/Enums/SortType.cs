using System.ComponentModel;

namespace Common.Enums;

public enum SortType : byte
{
    [Description("Ascending")]
    Asc = 1,

    [Description("Descending")]
    Desc = 2,
}