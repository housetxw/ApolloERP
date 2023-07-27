using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ae.AccountAuthority.Service.Common.Security
{
    public static class EncryptPassword
    {
        //盐的位数
        private const int SaltByteSize = 2 << 3;
        //32位字节生成36位Hash值
        private const int HashByteSize = 2 << 4;


        /// <summary>
        /// 创建密码对应Hash值
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Hashtable CreatePasswordHash(string password)
        {
            using (var rngcsp = new RNGCryptoServiceProvider())
            {
                var salt = new byte[SaltByteSize];
                rngcsp.GetBytes(salt);

                var iteration = new Random().Next(1, 2 << 6);
                var hash = Pbkdf2(password, salt, iteration, HashByteSize);
                var hashTable = new Hashtable
                {
                    {"Password", Convert.ToBase64String(hash) },
                    {"PasswordSalt", Convert.ToBase64String(salt) },
                    {"PasswordIteration", iteration }
                };
                return hashTable;
            }
        }

        /// <summary>
        /// 验证密码是否匹配
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public static bool ValidatePassword(string password, Hashtable passwordHash)
        {
            var hash = Convert.FromBase64String(passwordHash["Password"].ToString());
            var salt = Convert.FromBase64String(passwordHash["PasswordSalt"].ToString());
            var iteration = int.Parse(passwordHash["PasswordIteration"].ToString());

            var tempHash = Pbkdf2(password, salt, iteration, hash.Length);
            return SlowEqual(hash, tempHash);
        }

        /// <summary>
        /// 对比两个Byte[]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool SlowEqual(IReadOnlyList<byte> a, IReadOnlyList<byte> b)
        {
            var diff = (uint)a.Count ^ (uint)b.Count;
            for (var i = 0; i < a.Count && i < b.Count; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        /// <summary>
        /// 根据PBKDF2算法计算密码的哈希值
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iteration"></param>
        /// <param name="outputByte"></param>
        /// <returns></returns>
        private static byte[] Pbkdf2(string password, byte[] salt, int iteration, int outputByte)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iteration))
            {
                return pbkdf2.GetBytes(outputByte);
            }
        }

    }
}
