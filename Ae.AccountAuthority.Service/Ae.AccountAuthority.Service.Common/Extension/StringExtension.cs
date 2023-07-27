using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Ae.AccountAuthority.Service.Common.Constant;

namespace Ae.AccountAuthority.Service.Common.Extension
{
    public static class StringExtension
    {
        #region Base Type Extension

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
            return value.Equals(destination, StringComparison.OrdinalIgnoreCase);
        }

        public static bool NotEqualsIgnoreCase(this string value, string destination)
        {
            return !value.Equals(destination, StringComparison.OrdinalIgnoreCase);
        }

        public static bool CheckIsGuidFormat(this string guidStr)
        {
            return !guidStr.IsNullOrWhiteSpace() && Guid.TryParse(guidStr, out Guid newGuid);
        }

        public static string GetDefaultPassword(this string value, int length = 8)
        {
            return value.IsNullOrWhiteSpace() ? value : value.Substring(value.Length - 8);
        }

        public static string GenArgumentErrorInfo(this string value, string expString = "")
        {
            if (expString.IsNullOrWhiteSpace()) expString = CommonConstant.ArgumentValidateFailed;
            return $"\n{expString}\n{CommonConstant.ParameterReqDetail}\n{value}";
        }

        public static string GenExceptionInfo(this string value, string expType = "")
        {
            if (expType.IsNullOrWhiteSpace()) expType = CommonConstant.ExceptionOccured;
            return $"\n{expType}\n{CommonConstant.ParameterReqDetail}\n{value}";
        }

        public static string GenDBExceptionInfo(this string value, string expString)
        {
            return $"\n{expString}\n{CommonConstant.ParameterReqDetail}\n{value}";
        }

        #endregion Base Type Extension

        #region 正则表达式

        #region IsMatch(是否匹配正则表达式)
        /// <summary>
        /// 确定所指定的正则表达式在指定的输入字符串中是否找到了匹配项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsMatch(this string value, string pattern)
        {
            if (value == null)
            {
                return false;
            }
            return Regex.IsMatch(value, pattern);
        }
        /// <summary>
        /// 确定所指定的正则表达式在指定的输入字符串中找到匹配项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">规则</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsMatch(this string value, string pattern, RegexOptions options)
        {
            return value != null && Regex.IsMatch(value, pattern, options);
        }
        #endregion IsMatch(是否匹配正则表达式)

        #endregion 正则表达式


    }
}
