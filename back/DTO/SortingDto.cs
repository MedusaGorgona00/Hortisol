using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace DTO;

/// <summary>
/// Sorting model
/// </summary>
public sealed record SortingDto
{
    /// <summary>
    /// Type of sorting
    /// </summary>
    public SortType? SortType { get; init; }

    /// <summary>
    /// Sorting by field
    /// </summary>
    public string Field { get; init; }
}