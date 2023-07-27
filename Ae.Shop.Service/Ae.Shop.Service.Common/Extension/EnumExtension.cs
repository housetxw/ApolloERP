using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Ae.Shop.Service.Common.Extension
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取Enum的Display
        /// </summary>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public static string GetEnumDisplayName(this Enum enumVal)
        {
            string value = enumVal.ToString();
            FieldInfo field = enumVal.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (objs.Length == 0)
            {
                return value;
            }

            DisplayAttribute displayAttribute = (DisplayAttribute)objs[0];
            return displayAttribute.Name;
        }

        /// <summary>
        /// 获取Enum的Description
        /// </summary>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum enumVal)
        {
            string value = enumVal.ToString();
            FieldInfo field = enumVal.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs.Length == 0)
            {
                return value;
            }

            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }

        /// <summary>
        /// 枚举转整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this Enum value)
        {
            return (int)Enum.ToObject(value.GetType(), value);
        }

    }
}
