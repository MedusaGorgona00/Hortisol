using Common.Enums;
using DTO.Extensions;

namespace DTO.States;

/// <summary>
/// 
/// </summary>
public static class WashingtonDto
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Self : BaseStateDto
    {
        /// <summary>
        /// 
        /// </summary>
        public override string Name => StateType.Washington.ToString();

        /// <summary>
        /// 
        /// </summary>
        public override EnumDto[] Filters => new []
        {
            FilterType.Total.Dto(),
            FilterType.ByGrade.Dto(),
            FilterType.ByCompany.Dto(),
        };
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed record Total
    {
        /// <summary>
        /// 
        /// </summary>
        public float? Round { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Num { get; init; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed record ByGrade
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Grade { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public float? Round { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Num { get; init; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed record ByCompany
    {
        /// <summary>
        /// 
        /// </summary>
        public string Company { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string City { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Sum { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public float? UsTonns { get; init; }
    }
}
