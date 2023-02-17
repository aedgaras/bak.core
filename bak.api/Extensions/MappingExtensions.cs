using System.ComponentModel;
using AutoMapper;
using bak.api.Configurations;

namespace bak.api.Extensions;

public static class MappingExtensions
{
    public static string GetDescription<T>(this T entity)
    {
        var type = typeof(T);
        var memInfo = type.GetMember(entity.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return ((DescriptionAttribute)attributes[0]).Description;
    }
}