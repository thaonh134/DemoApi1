using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Enums
{
    public static class EnumHelper
    {
        public static string DescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static int DefaultValueAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DefaultValueAttribute[] attributes = (DefaultValueAttribute[])fi.GetCustomAttributes(
                typeof(DefaultValueAttribute), false);

            if (attributes != null && attributes.Length > 0) return (int)attributes[0].Value;
            else return 0;
        }
    }
}
