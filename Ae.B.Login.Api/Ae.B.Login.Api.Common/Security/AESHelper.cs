using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ApolloErp.Log;

namespace Ae.B.Login.Api.Common.Security
{
    /// <summary>
    /// 用于密码传输使用
    /// </summary>
    public static class AESHelper
    {
        ///密钥,长度为80的字符串
        private static string key = "r*(_gY;UiK^h!~(Y;r*(_gY;UiK^h!UF;N<>/)(_+=dff!~(Y;r*(_gY;UiK^h!UF&8YTjkhf./8.;RG";

        /// 偏移量,长度为16的字符串
        private static int ivT = 2 << 3;
        private static string iv = key.Substring(key.Length - ivT);

        /// <summary>AES加密</summary> 
        /// <param name="text">明文</param> 
        /// <returns>密文</returns> 
        public static string EncodeAES(string text)
        {
            if (key.Length > ivT)
            {
                iv = key.Substring(key.Length - ivT);
                key = key.Substring(0, ivT);
            }
            return AESEncry(text);
        }
        private static string AESEncry(string text)
        {
            try
            {
                RijndaelManaged rijndaelCipher = new RijndaelManaged();

                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = 128;
                rijndaelCipher.BlockSize = 128;

                byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
                byte[] keyBytes = new byte[16];
                int len = pwdBytes.Length;
                if (len > keyBytes.Length)
                {
                    len = keyBytes.Length;
                }

                Array.Copy(pwdBytes, keyBytes, len);
                rijndaelCipher.Key = keyBytes;
                rijndaelCipher.IV = Encoding.UTF8.GetBytes(iv);
                ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(text);

                byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
                return Convert.ToBase64String(cipherBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        /// <summary>AES解密</summary> 
        /// <param name="text">密文</param> 
        /// <returns>明文</returns> 
        public static string DecodeAES(string text)
        {
            try
            {
                RijndaelManaged rijndaelCipher = new RijndaelManaged();

                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = 128;
                rijndaelCipher.BlockSize = 128;

                byte[] encryptedData = Convert.FromBase64String(text);
                byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
                byte[] keyBytes = new byte[16];

                int len = pwdBytes.Length;
                if (len > keyBytes.Length)
                { len = keyBytes.Length; }

                Array.Copy(pwdBytes, keyBytes, len);
                rijndaelCipher.Key = keyBytes;
                rijndaelCipher.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();

                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                return Encoding.UTF8.GetString(plainText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }
    }
}
