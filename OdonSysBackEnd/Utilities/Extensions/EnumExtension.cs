using System.ComponentModel;

namespace Utilities.Extensions;

public static class EnumExtension
{
    public static string GetDescription(this Enum value)
    {
        if (value == null)
        {
            return null;
        }

        var field = value.GetType().GetField(value.ToString());
        var attribs = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
        return attribs.Length > 0 ? ((DescriptionAttribute)attribs[0]).Description : value.ToString();
    }
}
