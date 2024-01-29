using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Ae.FileUpload.Api.Common.Constant;

namespace Ae.FileUpload.Api.Common.Extension
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
            return destination.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool NotEqualsIgnoreCase(this string value, string destination)
        {
            return !destination.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool CheckIsGuidFormat(this string guidStr)
        {
            return !guidStr.IsNullOrWhiteSpace() && Guid.TryParse(guidStr, out Guid newGuid);
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
            return value != null && Regex.IsMatch(value, pattern);
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
