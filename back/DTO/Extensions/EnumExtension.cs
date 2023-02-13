using System;
using Common.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DTO.Extensions;

public static class EnumExtension
{
    public static EnumDto Dto<T>(this T x) where T : struct, Enum
    {
        return new EnumDto()
        {
            Id = Convert.ToByte(x),
            Name = x.GetAttr<T, DisplayAttribute>(x => x.GetName()),
            Description = x.GetAttr<T, DescriptionAttribute>(x => x.Description),
        };
    }
}
