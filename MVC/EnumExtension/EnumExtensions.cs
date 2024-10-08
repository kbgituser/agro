using System.ComponentModel;

namespace MVC.EnumExtension;

public static class EnumExtensions
{
    public static string ToDescription(this System.Enum data)
    {
        var field = data.GetType().GetField(data.ToString());
        var desAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        if (desAttribute.Length > 0)
        {
            return desAttribute[0].Description;
        }
        return string.Empty;
    }
}
