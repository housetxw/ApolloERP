using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.Login.Api.Common
{
    public class FormatValite
    {
        /// <summary>
        /// 验证电话号码的主要代码如下：
        /// </summary>
        /// <param name="str_telephone"></param>
        /// <returns></returns>
        public static bool IsTelephone(string str_telephone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"^(\d{3,4}-)?\d{6,8}$");
        }


        /// <summary>
        /// 验证手机号码的主要代码如下：
        /// </summary>
        /// <param name="str_handset"></param>
        /// <returns></returns>
        public static bool IsMobile(string str_mobile)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_mobile, @"^1[34578]\\d{9}$");
        }


        /// <summary>
        /// 验证身份证号的主要代码如下：
        /// </summary>
        /// <param name="str_idcard"></param>
        /// <returns></returns>
        public static bool IsIDcard(string str_idcard)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_idcard, @"(^\d{18}$)|(^\d{15}$)");
        }

        /// <summary>
        /// 验证输入为数字的主要代码如下：
        /// </summary>
        /// <param name="str_number"></param>
        /// <returns></returns>
        public static bool IsNumber(string str_number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number, @"^[0-9]*$");
        }

        /// <summary>
        /// 验证邮编的主要代码如下：
        /// </summary>
        /// <param name="str_postalcode"></param>
        /// <returns></returns>
        public static bool IsPostalcode(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_postalcode, @"^\d{6}$");
        }
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="str_Email"></param>
        /// <returns></returns>
        public static bool IsEmail(string str_Email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Email, @"\\w{1,}@\\w{1,}\\.\\w{1,}");
        }
    }
}
