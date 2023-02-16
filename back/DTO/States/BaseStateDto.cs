namespace DTO.States;

/// <summary>
/// Base state model
/// </summary>
public abstract class BaseStateDto
{
    /// <summary>
    /// Identifier
    /// </summary>
    public virtual int Id { get; }

    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; }

    /// <summary>
    /// Allowed filters
    /// </summary>
    public virtual FilterDto[] Filters { get; }
}
