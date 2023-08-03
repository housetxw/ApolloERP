using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Ae.Receive.Service.Common.Helper
{
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举值上的Description特性的说明
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="obj">枚举值</param>
        /// <returns>特性的说明</returns>
        public static string GetEnumDescription<T>(T obj)
        {
            var type = obj.GetType();
            FieldInfo field = type.GetField(Enum.GetName(type, obj));
            DescriptionAttribute descAttr =
                Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (descAttr == null)
            {
                return string.Empty;
            }

            return descAttr.Description;
        }
    }
}
