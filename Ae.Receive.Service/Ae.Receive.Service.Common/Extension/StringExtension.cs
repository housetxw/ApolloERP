using System;
using System.Collections.Generic;
using System.Text;
using Ae.Receive.Service.Common.Constant;

namespace Ae.Receive.Service.Common.Extension
{
    public static class StringExtension
    {
        // ---------------------------------- Base Type Extension --------------------------------------
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool EqualsIgnoreCase(this string value, string destination)
        {
            return value.Equals(destination, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool NotEqualsIgnoreCase(this string value, string destination)
        {
            return !value.Equals(destination, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool CheckIsGuidFormat(this string guidStr)
        {
            return !guidStr.IsNullOrWhiteSpace() && Guid.TryParse(guidStr, out Guid newGuid);
        }

        public static string GenArgumentErrorInfo(this string value, string expString = "")
        {
            if (expString.IsNullOrWhiteSpace()) expString = CommonConstant.ArgumentValidateFailed;
            return $"{expString}\n{CommonConstant.ParameterReqDetail}\n{value}";
        }

        public static string GenExceptionInfo(this string value, string expType = "")
        {
            if (expType.IsNullOrWhiteSpace()) expType = CommonConstant.ExceptionOccured;
            return $"{expType}\n{CommonConstant.ParameterReqDetail}\n{value}";
        }

        public static string GenDBExceptionInfo(this string value, string expString)
        {
            return $"{expString}\n{CommonConstant.ParameterReqDetail}\n{value}";
        }

        /// <summary>
        /// 字符串转换未枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static Enum CovertToEnum<T>(this string enumValue) where T : Enum
        {
            T t = default(T);

            bool tryResult = Enum.TryParse(t.GetType(), enumValue, out object result);

            if (tryResult)
                return (Enum)result;
            return t;
        }



    }
}
