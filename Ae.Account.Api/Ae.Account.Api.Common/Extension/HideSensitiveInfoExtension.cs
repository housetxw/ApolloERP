using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Account.Api.Common.Extension
{
    public static class HideSensitiveInfoExtension
    {
        /// <summary>
        /// 脱敏手机号中间四位
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static string HidePhoneSensitiveInfo(this string phone, int left = 3, int right = 4)
        {
            if (phone.IsNullOrWhiteSpace() || !phone.IsTelephone()) return phone;
            return phone.HideSensitiveInfo(left, right);
        }

        /// <summary>
        /// 隐藏敏感信息
        /// </summary>
        /// <param name="info">信息实体</param>
        /// <param name="left">左边保留的字符数</param>
        /// <param name="right">右边保留的字符数</param>
        /// <param name="basedOnLeft">当长度异常时，是否显示左边 </param>
        /// <returns></returns>
        public static string HideSensitiveInfo(this string info, int left, int right, bool basedOnLeft = true)
        {
            if (info.IsNullOrWhiteSpace())
            {
                return "";
            }

            StringBuilder sbText = new StringBuilder();
            int hiddenCharCount = info.Length - left - right;

            if (hiddenCharCount > 0)
            {
                string prefix = info.Substring(0, left), suffix = info.Substring(info.Length - right);
                sbText.Append(prefix);
                for (int i = 0; i < hiddenCharCount; i++)
                {
                    sbText.Append("*");
                }
                sbText.Append(suffix);
            }
            else
            {
                if (basedOnLeft)
                {
                    if (info.Length > left && left > 0)
                    {
                        sbText.Append(info.Substring(0, left) + "****");
                    }
                    else
                    {
                        sbText.Append(info.Substring(0, 1) + "****");
                    }
                }
                else
                {
                    if (info.Length > right && right > 0)
                    {
                        sbText.Append("****" + info.Substring(info.Length - right));
                    }
                    else
                    {
                        sbText.Append("****" + info.Substring(info.Length - 1));
                    }
                }
            }
            return sbText.ToString();
        }

        /// <summary>
        /// 隐藏敏感信息
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="subLen">信息总长与左子串（或右子串）的比例</param>
        /// <param name="basedOnLeft">当长度异常时，是否显示左边，默认true，默认显示左边</param>
        /// <code>true</code>显示左边，<code>false</code>显示右边
        /// <returns></returns>
        public static string HideSensitiveInfo(this string info, int subLen = 3, bool basedOnLeft = true)
        {
            if (info.IsNullOrWhiteSpace())
            {
                return "";
            }

            string prefix;
            string suffix;

            if (subLen <= 1)
            {
                subLen = 3;
            }

            int subLength = info.Length / subLen;
            if (subLength > 0 && info.Length > (subLength * 2))
            {
                prefix = info.Substring(0, subLength);
                suffix = info.Substring(info.Length - subLength);
                return prefix + "****" + suffix;
            }

            if (basedOnLeft)
            {
                prefix = subLength > 0 ? info.Substring(0, subLength) : info.Substring(0, 1);
                return prefix + "****";
            }

            suffix = subLength > 0 ? info.Substring(info.Length - subLength) : info.Substring(info.Length - 1);
            return "****" + suffix;
        }

        /// <summary>
        /// 隐藏邮件详情
        /// </summary>
        /// <param name="email">邮件地址</param>
        /// <param name="left">邮件头保留字符个数，默认值设置为3</param>
        /// <returns></returns>
        public static string HideEmailDetail(this string email, int left = 3)
        {
            if (email.IsNullOrWhiteSpace())
            {
                return "";
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))//如果是邮件地址
            {
                int suffixLen = email.Length - email.LastIndexOf('@');
                return HideSensitiveInfo(email, left, suffixLen, false);
            }

            return HideSensitiveInfo(email);
        }

    }
}
