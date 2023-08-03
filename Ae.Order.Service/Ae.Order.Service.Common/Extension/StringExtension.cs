using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Common.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 字符串转换未枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static Enum CovertToEnum<T>(this string enumValue) where T : Enum
        {
            T t = default(T);
      
           bool tryResult= Enum.TryParse(t.GetType(), enumValue, out object result);

            if (tryResult)
                return (Enum)result;
            return t;
        }

    }
}
