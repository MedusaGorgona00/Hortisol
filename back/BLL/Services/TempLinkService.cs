using System.IO;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Helpers;

namespace BLL.Services;

internal static class TempLinkService
{
    internal static async Task<string> Create((Stream Source, string Name) file, string userId)
    {
        var dir = AppConstants.BaseDir.Combine(AppConstants.TempSuffixOfPath, userId);
        dir.CreateDirectoryIfNotExist();

        var path = dir.Combine(file.Name);
        await (path, file.Source).SaveStreamByPath();

        return $"{AppConstants.BaseUri}{path.SelectSuffixFromFiles()}";
    }

    internal static async Task<string> Create((Stream Source, string Name) file)
    {
        return await Create(file, RandomString.LowerAlpha(5));
    }
}
