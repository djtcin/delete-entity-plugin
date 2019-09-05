using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteEntityPlugin.Helpers
{
    public class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            System.Reflection.FieldInfo info = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = info.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
