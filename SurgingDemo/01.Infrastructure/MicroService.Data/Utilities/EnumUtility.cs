
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MicroService.Data.Utilities
{
    public class EnumUtility
    {
        public static string GetDescriptions(Enum source)
        {
            var name = source.ToString();
            foreach (var memberInfo in source.GetType().GetMember(name))
            {
                var attribute = (DescriptionAttribute)memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                var description = attribute != null ? attribute.Description : name;
                return description;
            }
            return name;
        }

        public static string GetDescriptionFromValue(Enum source, string value)
        {
            var name = source.ToString();
            foreach (var memberInfo in source.GetType().GetMember(name))
            {
                var attributes = (DescriptionAttribute[])memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                foreach (var descriptionsAttribute in attributes)
                {
                    var description = descriptionsAttribute != null ? descriptionsAttribute.Description : name;
                    return description;
                }
            }
            return name;
        }

        public static string GetDescription(Enum source, string value)
        {
            var enumType = source.GetType();
            foreach (Enum enumSource in Enum.GetValues(enumType))
            {
                var enumSourceValue = enumSource.ToString();
                if (enumSourceValue.Equals(value))
                {
                    return GetDescriptions(enumSource);
                }
            }

            return value;
        }

        public static string GetDescriptionByName<T>(T enumItemName)
        {
            FieldInfo fi = enumItemName.GetType().GetField(enumItemName.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return enumItemName.ToString();
        }
    }
}
