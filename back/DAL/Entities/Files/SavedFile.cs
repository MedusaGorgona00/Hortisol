using System.ComponentModel.DataAnnotations;
using Common.Interfaces;

namespace DAL.Entities.Files;

public class SavedFile : ISavedFile
{
    public int Id { get; set; }

    [MaxLength(128)]
    public string Name { get; set; }

    [MaxLength(512)]
    public string Path { get; set; }

    [MaxLength(16)]
    public byte[] Md5 { get; set; }
}
