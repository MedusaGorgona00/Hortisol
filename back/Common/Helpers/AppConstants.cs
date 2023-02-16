using System;
using System.Collections.Generic;
using Common.Enums;
using Common.Extensions;

namespace Common.Helpers;

public static class AppConstants
{
    #region VirtualDir
    public static string BaseDir { get; set; }
    #endregion

    #region ServiceUri.Self
    public static string BaseUri { get; set; }

    public static string BaseFrontUri { get; set; }
    #endregion

    #region RefreshToken
    public static byte[] Key { get; } = "8d774328bbcf413c95662aa223341e01".ToBytes();

    public static byte[] Iv { get; } = "eefd2c33e23d48039d1fafcab7502257".ToBytes();
    #endregion

    #region SuffixOfPaths
    /// <summary>
    /// Temp
    /// </summary>
    public static string TempSuffixOfPath { get; } = "Temp";
    #endregion

    #region Report
    public static Dictionary<StateType, Dictionary<FilterType, string>> DefaultSort
        => new()
        {
            {
                StateType.Washington,
                new Dictionary<FilterType, string>
                {
                    {
                        FilterType.Total,
                        ""
                    },
                    {
                        FilterType.ByGrade,
                        "order by \"Grade\" nulls last"
                    },
                    {
                        FilterType.ByCompany,
                        "order by p.partner nulls last, p.city"
                    },
                }
            },
            {
                StateType.Illinois,
                new Dictionary<FilterType, string>
                {
                    {
                        FilterType.SmallPackage,
                        "order by county nulls last, \"Product\""
                    },
                    {
                        FilterType.LargePackage,
                        "order by county nulls last, \"N\", \"P\", \"K\""
                    },
                }
            }
        };

    public static Dictionary<StateType, Dictionary<FilterType, string[]>> AllowedSort
        => new()
        {
            {
                StateType.Washington,
                new Dictionary<FilterType, string[]>
                {
                    {
                        FilterType.Total,
                        new []{ "Row", "Round", "Num" }
                    },
                    {
                        FilterType.ByGrade,
                        new []{ "Row", "Grade", "Weight" }
                    },
                    {
                        FilterType.ByCompany,
                        new []{ "Row", "Company", "City", "Weight" }
                    },
                }
            },
            {
                StateType.Illinois,
                new Dictionary<FilterType, string[]>
                {
                    {
                        FilterType.SmallPackage,
                        new []{ "Row", "Product", "County", "Weight" }
                    },
                    {
                        FilterType.LargePackage,
                        new []{ "Row", "N", "P", "K", "County", "Weight" }
                    },
                }
            }
        };

    public static Dictionary<StateType, FilterType[]> AllowedFilters
        => new()
        {
            {
                StateType.Washington,
                new[]
                {
                    FilterType.Total,
                    FilterType.ByGrade,
                    FilterType.ByCompany,
                }
            },
            {
                StateType.Illinois,
                new[]
                {
                    FilterType.SmallPackage,
                    FilterType.LargePackage,
                }
            },
        };
    #endregion
}
