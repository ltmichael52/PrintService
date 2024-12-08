using System.ComponentModel;

namespace PrintService.Extentions;

public static class StringExtention
{
    #region -- Methods -- 

    /// <summary>
    /// Convert a string value to enum value
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    /// <param name="value">Description or value need to convert</param>
    /// <param name="default">Default value</param>
    /// <returns>Return the enum value</returns>
    public static T? ToEnum<T>(this string? value, T @default)
    {
        var res = @default;

        if (!typeof(T).IsEnum)
        {
            return res;
        }

        var type = typeof(T);
        var l = type.GetFields();

        foreach (var i in l)
        {
            if (Attribute.GetCustomAttribute(i, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (i.Name == value)
                {
                    res = (T?)i.GetValue(null);
                    break;
                }

                if (attribute.Description == value)
                {
                    res = (T?)i.GetValue(null);
                    break;
                }
            }
            else
            {
                if (i.Name == value)
                {
                    res = (T?)i.GetValue(null);
                    break;
                }
            }
        }

        return res;
    }

    #endregion  
}