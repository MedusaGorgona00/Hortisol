using Common.Enums;
using DTO.Extensions;

namespace DTO.States;

/// <summary>
/// 
/// </summary>
public static class IllinoisDto
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Self : BaseStateDto
    {
        /// <summary>
        /// 
        /// </summary>
        public override string Name => StateType.Illinois.ToString();

        /// <summary>
        /// 
        /// </summary>
        public override EnumDto[] Filters => new[]
        {
            FilterType.SmallPackage.Dto(),
            FilterType.LargePackage.Dto(),
        };
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed record SmallPackage
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Product { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string? County { get; init; }
        
        /// <summary>
        /// 
        /// </summary>
        public float? Weight { get; init; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed record LargePackage
    {
        /// <summary>
        /// 
        /// </summary>
        public int? N { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public int? P { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public int? K { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string County { get; init; }
        
        /// <summary>
        /// 
        /// </summary>
        public float? Weight { get; init; }
    }
}
