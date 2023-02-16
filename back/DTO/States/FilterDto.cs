namespace DTO.States;

/// <summary>
/// Filter with scheme
/// </summary>
public class FilterDto
{
    /// <summary>
    /// Filter information
    /// </summary>
    public virtual EnumDto FilterType { get; set; }

    /// <summary>
    /// Response scheme of this filter type
    /// </summary>
    public virtual object Scheme { get; set; }
}