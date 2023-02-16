using Common.Enums;
using Common.Extensions;
using DTO.Extensions;
using DTO.Interfaces;

namespace DTO.States;

/// <summary>
/// General Illinois model
/// </summary>
public static class IllinoisDto
{
    /// <summary>
    /// Illinois self model with allowed filters
    /// </summary>
    public sealed class Self : BaseStateDto
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public override int Id => StateType.Illinois.ToInt();

        /// <summary>
        /// Name
        /// </summary>
        public override string Name => StateType.Illinois.ToString();

        /// <summary>
        /// Allowed filters
        /// </summary>
        public override FilterDto[] Filters => new FilterDto[]
        {
            new()
            {
                FilterType = FilterType.SmallPackage.Dto(),
                Scheme = new SmallPackage()
            },
            new()
            {
                FilterType = FilterType.LargePackage.Dto(),
                Scheme = new LargePackage(),
            }
        };
    }

    /// <summary>
    /// Filter small package model
    /// </summary>
    public sealed class SmallPackage : IIdHas
    {
        /// <summary>
        /// Row number
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Product
        /// </summary>
        public string? Product { get; init; }

        /// <summary>
        /// County
        /// </summary>
        public string? County { get; init; }
        
        /// <summary>
        /// Weight
        /// </summary>
        public float? Weight { get; init; }
    }

    /// <summary>
    /// Filter large package model
    /// </summary>
    public sealed class LargePackage : IIdHas
    {
        /// <summary>
        /// Row number
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// N-nitrogen fertilizer
        /// </summary>
        public int? N { get; init; }

        /// <summary>
        /// P-phosphorus fertilizer
        /// </summary>
        public int? P { get; init; }

        /// <summary>
        /// K-potassium fertilizer
        /// </summary>
        public int? K { get; init; }

        /// <summary>
        /// County
        /// </summary>
        public string County { get; init; }
        
        /// <summary>
        /// Wight
        /// </summary>
        public float? Weight { get; init; }
    }
}
