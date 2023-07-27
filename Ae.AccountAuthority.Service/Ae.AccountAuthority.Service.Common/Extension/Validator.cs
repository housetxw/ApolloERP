using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ae.AccountAuthority.Service.Common.Extension
{
    public static class Validator
    {
        /// <summary>
        /// 是否Url地址（统一资源定位）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUrl(this string value)
        {
            return value.IsNotNullOrWhiteSpace() &&
                   value.IsMatch(
                       @"^(https|http)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|cn|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$",
                       RegexOptions.IgnoreCase);
        }

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
