namespace BlazorMasterMind.Extensions;
using System.ComponentModel;
using System.Reflection;


public static class EnumExtensions
{
    public static string GetDescription(this Enum e)
    {
        FieldInfo? field = e?.GetType()?.GetField(e.ToString());

        if (field != null)
        {
            DescriptionAttribute? attr = field.GetCustomAttribute<DescriptionAttribute>();
            if (attr != null)
            {
                return attr.Description;
            }
        }
        return e?.ToString() ?? string.Empty;
    }

    public static T GetNextValue<T>(this Enum e)
        where T : Enum
    {
        T[] enumValues = (T[])Enum.GetValues(e.GetType());
        int index = Array.IndexOf(enumValues, e);

        if (index >= enumValues.Length - 1)
            return enumValues[0];
        else
            return enumValues[index + 1];
    }

    public static T[] ToArray<T>(this Enum e)
    {
        return (T[])Enum.GetValues(e.GetType());
    }

}
