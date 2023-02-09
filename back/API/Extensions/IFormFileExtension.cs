using System.IO;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Extensions;
using Microsoft.AspNetCore.Http;

namespace API.Extensions;

internal static class IFormFileExtension
{
    internal static async Task<(Stream Source, string FileName)> ToStream(this IFormFile file)
    {
        if (file.IsNull())
            throw new InnerException($"2591. The file isn't attached", nameof(file));

        var source = new MemoryStream();
        await file.CopyToAsync(source);

        return (source, file.FileName);
    }
}
