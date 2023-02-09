using System.Collections.Generic;
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

    public static string ExcelContentType => "application/vnd.ms-excel";
}
