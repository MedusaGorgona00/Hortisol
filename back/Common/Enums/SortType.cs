using System.ComponentModel;

namespace Common.Enums;

public enum SortType : byte
{
    [Description("Ascending")]
    Ascending = 1,

    [Description("Descending")]
    Descending = 2,
}