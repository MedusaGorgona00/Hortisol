using System.ComponentModel.DataAnnotations;
using DTO.Interfaces;

namespace DTO;

public static class SavedFileDto
{
    public sealed record EditIn<T>
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public T File { get; init; }
    }

    public sealed record ListIdOut: ISaveFileOut
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public string Link { get; init; }
    }

    public sealed record ByIdOut : ISaveFileOut
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public string Link { get; init; }
    }
}
