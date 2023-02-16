using Common.Enums;
using Common.Extensions;
using DTO.Extensions;

namespace DTO.States;

/// <summary>
/// General Washington model
/// </summary>
public static class WashingtonDto
{
    /// <summary>
    /// Washington self model with allowed filters
    /// </summary>
    public sealed class Self : BaseStateDto
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public override int Id => StateType.Washington.ToInt();

        /// <summary>
        /// Name
        /// </summary>
        public override string Name => StateType.Washington.ToString();

        /// <summary>
        /// Allowed filters
        /// </summary>
        public override FilterDto[] Filters => new FilterDto[]
        {
            new()
            {
                FilterType = FilterType.Total.Dto(),
                Scheme = new Total()
            },
            new()
            {
                FilterType = FilterType.ByGrade.Dto(),
                Scheme = new ByGrade(),
            },
            new()
            {
                FilterType = FilterType.ByCompany.Dto(),
                Scheme = new ByCompany(),
            }
        };
    }

    /// <summary>
    /// Filter total model
    /// </summary>
    public sealed class Total
    {
        /// <summary>
        /// Row number
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Round
        /// </summary>
        public float? Round { get; init; }

        /// <summary>
        /// Sum
        /// </summary>
        public decimal? Num { get; init; }
    }

    /// <summary>
    /// Filter by grade model
    /// </summary>
    public sealed class ByGrade
    {
        /// <summary>
        /// Row number
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Grade
        /// </summary>
        public string? Grade { get; init; }

        /// <summary>
        /// Weight
        /// </summary>
        public float? Weight { get; init; }
    }

    /// <summary>
    /// Filter by company model
    /// </summary>
    public sealed class ByCompany
    {
        /// <summary>
        /// Row number
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        public string Company { get; init; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; init; }

        /// <summary>
        /// Wight
        /// </summary>
        public float? Weight { get; init; }
    }
}
