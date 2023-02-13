using Common.Interfaces;

namespace DTO;

/// <summary>
/// 
/// </summary>
public sealed record EnumDto : IEnum
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string Description { get; init; }
}
