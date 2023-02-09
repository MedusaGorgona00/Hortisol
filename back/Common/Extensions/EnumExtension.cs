using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Extensions;

public static class EnumExtension
{
    public static string Description<T>(this T enumerationValue) where T : struct
    {
        var defaultDescription = enumerationValue.ToString();
        var memberInfo = enumerationValue.GetType().GetMember(defaultDescription);
        if (memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;
        }

        return defaultDescription;
    }

    public static string Display<T>(this T enumerationValue) where T : struct
    {
        var defaultDescription = enumerationValue.ToString();
        var memberInfo = enumerationValue.GetType().GetMember(defaultDescription);
        if (memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attrs.Length > 0)
                return ((DisplayAttribute)attrs[0]).GetName();
        }

        return defaultDescription;
    }
}
