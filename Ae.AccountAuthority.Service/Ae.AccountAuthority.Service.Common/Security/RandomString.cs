using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Ae.AccountAuthority.Service.Common.Constant;

namespace Ae.AccountAuthority.Service.Common.Security
{
    public static class RandomString
    {
        public static string GenResetPasswordRandomString(Dictionary<int, int> dic = null)
        {
            return CreateRandomStringWithDic(dic);
        }

        public static string GenResetPasswordRandomStringWithLength(int len = 8)
        {
            return CreateRandomStringWithLength(len);
        }

        public static string CreateRandomStringWithDic(Dictionary<int, int> dic = null)
        {
            if (dic == null) { dic = CommonConstant.ResetPassword; }

            var pwd = "";
            var defPwd = "Apollo@Erp";
            try
            {
                if (dic != null)
                {
                    foreach (var (key, value) in dic)
                    {
                        var tempPwd = string.Empty;
                        string src;
                        switch (key)
                        {
                            case PasswordType.Number:
                                src = "23456789";
                                break;
                            case PasswordType.Word:
                                src = "abcdefghjkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
                                break;
                            case PasswordType.Symbol:
                                src = "@#%";
                                break;
                            default:
                                src = string.Empty;
                                break;
                        }
                        if (!string.IsNullOrEmpty(src))
                        {
                            var rnd = new Random(GetNewSeed());
                            int i = 0;
                            while (i++ < value)
                            {
                                tempPwd += src[rnd.Next(0, src.Length)].ToString();
                            }
                            pwd += tempPwd;
                        }
                    }
                    pwd = GetDisorderBytes(pwd);
                    if (string.IsNullOrWhiteSpace(pwd) || pwd.Length < 8)
                    {
                        pwd = defPwd;
                    }
                }
            }
            catch
            {
                pwd = defPwd;
            }
            //Regex rx = new Regex(@"(.)\1+");
            //MatchCollection matches = rx.Matches(pwd);
            //int a = matches.Count;
            //if (a > 0)
            //{
            //    pwd = CreateRandomStringWithDic(dic);
            //}
            return pwd;
        }
        /// <summary>
        /// 生成随机数的种子
        /// </summary>
        /// <returns></returns>
        private static int GetNewSeed()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var rndBytes = new byte[4];
                rng.GetBytes(rndBytes);
                return BitConverter.ToInt32(rndBytes, 0);
            }
        }
        private static string GetDisorderBytes(string str)
        {
            char[] c = str.ToCharArray();
            int min = 1;
            int max = c.Length;
            int inx = 0;
            char b = new char();
            Random rnd = new Random();
            while (min != max)
            {
                int r = rnd.Next(min++, max);
                b = c[inx];
                c[inx] = c[r];
                c[r] = b;
                inx++;
            }
            return new string(c);
        }

        /// <summary>
        /// 随机生成指定长度的数字+字符+@#%
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CreateRandomStringWithLength(int len = 8)
        {
            var s = "23456789@#%abcdefghjkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
            var reValue = string.Empty;
            var rnd = new Random(GetNewSeed());
            while (reValue.Length < len)
            {
                var s1 = s[rnd.Next(0, s.Length)].ToString();
                if (reValue.IndexOf(s1, StringComparison.Ordinal) == -1) reValue += s1;
            }
            return reValue;
        }
    }

    /// <summary>
    /// 密码类型
    /// </summary>
    public static class PasswordType
    {
        public const int Number = 1;

        public const int Word = 2;

        public const int Symbol = 3;
    }

}
