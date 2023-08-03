using System.ComponentModel;
using System;
using System.Reflection;

namespace Ae.C.MiniApp.Api.Common.Extension
{
    public static class EnumExtension
    {
        /// <summary>
        /// 根据DescriptionAttribute获取枚举描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();   //获取类型  
            MemberInfo[] memberInfos = type.GetMember(value.ToString());   //获取成员  
            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];   //获取描述特性  
                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;    //返回当前描述
                }
            }
            return value.ToString();
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
