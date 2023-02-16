using System;
using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace DTO;

/// <summary>
/// Filter model
/// </summary>
public sealed record FiltrationDto
{
    /// <summary>
    /// Filter type
    /// </summary>
    [Required]
    public FilterType FilterType { get; init; }

    /// <summary>
    /// Start date
    /// </summary>
    public DateTime? StartDate { get; init; }

    /// <summary>
    /// End date
    /// </summary>
    public DateTime? EndDate { get; init; }
}
