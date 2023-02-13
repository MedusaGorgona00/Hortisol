namespace DTO.States;

/// <summary>
/// 
/// </summary>
public abstract class BaseStateDto
{
    /// <summary>
    /// 
    /// </summary>
    public virtual string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public virtual EnumDto[] Filters { get; }
}
