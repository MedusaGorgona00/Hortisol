using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace DTO;

/// <summary>
/// 
/// </summary>
public sealed record SortingDto
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public SortType Type { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string Field { get; init; }
}