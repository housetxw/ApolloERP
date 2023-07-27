using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ae.Shop.Service.Common.Extension
{
    public static class Validator
    {

        /// <summary>
        /// 验证是否是11位手机号格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTelephone(this string value)
        {
            return value.IsNotNullOrWhiteSpace() && Regex.IsMatch(value, @"^(0|86|17951)?(1)[0-9]{10}$");
            //return value.IsNotNullOrWhiteSpace() && Regex.IsMatch(value, @"^(0|86|17951)?(13[0-9]|15[012356789]|17[013678]|18[0-9]|14[57])[0-9]{8}$");
        }
    }
}
