using System;
using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace DTO;

/// <summary>
/// 
/// </summary>
public sealed record FiltrationDto
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public FilterType Type { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? StartDate { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? EndDate { get; init; }
}
