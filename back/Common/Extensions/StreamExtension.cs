using System.IO;
using System.Threading.Tasks;

namespace Common.Extensions;

public static class StreamExtension
{
    public static async Task SaveStreamByPath(this (string Path, Stream Source) x)
    {
        var file = x.Path.RecreateFile();

        x.Source.Seek(0, SeekOrigin.Begin);
        await x.Source.CopyToAsync(file);

        x.Source.Close();
        file.Close();
    }

    public static byte[] ToBytes(this Stream stream)
    {
        var ms = new MemoryStream();
        stream.CopyTo(ms);

        return ms.ToArray();
    }
}
