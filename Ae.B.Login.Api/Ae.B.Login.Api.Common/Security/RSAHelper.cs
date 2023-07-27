using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Ae.B.Login.Api.Common.Security
{
    /// <summary>
    /// 用于获取所属单位列表时使用
    /// RSA加解密 使用OpenSSL的公钥加密/私钥解密
    /// </summary>
    public class RSAHelper
    {
        private readonly RSA privateKeyRsaProvider;

        private readonly RSA publicKeyRsaProvider;

        private readonly HashAlgorithmName hashAlgorithmName;

        private readonly Encoding encoding;

        public const string RSA_PRIKEY = "MIIEpgIBAAKCAQEAxuORZeMcmXcF8WIDOlHGo6Rl1IOfQdBmb0HNyOdCFJHOgbsKxI/mzoFTHgHm0JrOgBdAT8JrQW0U9/B5zeguYdPvYumNmrjkIOeYzVcB/2x0h5I2gH+4chxM11A+oE0RIOCaUZV/iyMRi2et7Xz5300ArdoCdHo9JN4ZoZWa+NikEUqkkbwRis+aK4HXj7qmi6+YiCbtrcmpUVo3lGIl1WVYU1NwvcIet5SzcBG8Rv+AOqNAxwr5Z0aBw/hHpcLPleWCr6kGdc5renjjvbO7HO/MdVY4NF1WtCYL/WOYj8mlz6GsNa8GP1LHcbR8pV/+FGQ5aHBUP4RI1bS+ljoe4wIDAQABAoIBAQC7mLr0V5wKRPIeFEznHWs3v0EtilkVQvTbzD3GWhtCO6WFSF+HuBQEhIdZroGAEgTlXQkUMlXoiHNWykSZq37UV4tcyN8AKZVEVC8UwBTqT+o6VfeCjPX07CyuisoHeanGURQ+D70WCCXsUAcGEvO9ZGxXj2LxartxouyaNPrP7B5oulCqyNrRWSiRtCZBLgRhWslUesF04/BTAF2juZYk3xOu4+UDkvW7EThyGbFOs8OlgvLX2MCFWghxsBXDdJqG0e3VFW4G3wxhZJo7/hdFA+w4jIgGJv4gHtIVl5ERoHwR3YK6jDKTDO3qILnYgeBcPQDSGs9ZVEhyIEzpAOkBAoGBAOG/TCFPUwp3U85n0f9VB4hiW3BFPLwqn/D1z4V+Ef/rerBhBg5kemGNsMNvhu31oO69Ab2eHOzSw2+7fVC04kgi9w0fPH2BeHuEzyEyDr59Gryc60ITkIOcsGiidHNx8aGBIJNVJN+L4s2n0craUVwpKBCnWLxKjMGswRw7TtsTAoGBAOGK1+BvQ0TKDLv7fgyvoghUu7ZcinzaTqOfSnxmD8SEJqc53bfLcsUoAZujTcxlByj92iGLrWBxBMCuoMTVNAbJ6fejj1ejaOhe8rLOVcIR2sFXLBxAFFX2Cx05bquON2BM8Mtian2Bk/OY258JQs8/1Aq93Qu7LBDS+YkSNNbxAoGBAJ1wFp4cfmOMOQx4Z4JVQL2jKvYvs13ftFAOfr4w4EjFZ3lfESQ0ew1SDfce7tFCPwyBEJe3j6CylM51yb3hiPaaPAYQxNUa2HX8d6BFEvdStLTaE1Cv6FeBjZ5Lvj9NNiFWtutJtD+bhX+8DJoWjCtkIKcMrtwSefwQiVUxH5u1AoGBAIaIf0SMmZ+KthXbadC/YvN9N4sLoD/hKE9JUPDrIDSkbzoItkxPuuIHfw4LVvi4upCk+1xPKNgCEkes4pEGa8Yw4rKoiXRJMOaK3FHz6gudAtOY3+LqBjdQUEjaXT1P05BJ1Mg8qDC8/jer0jUHHSbqd90Aa733o81VSyuMBKDBAoGBAImeObMkylMdoFPkmNIIx5xR4M66nKZlRg/EXEB7Hd+qvme5WK6DijmGtfHv+QZb06K2miHRbVN1ygSSm2TxsO4qvyQKsG/4F1MkPmEbBcQHyjbw9Cvebqpqep5ucB2Psl/n5nMifjk2HxVcYCr1GSeywplsRNnxX389WcgUofnb";

        public const string RSA_PUBKEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxuORZeMcmXcF8WIDOlHGo6Rl1IOfQdBmb0HNyOdCFJHOgbsKxI/mzoFTHgHm0JrOgBdAT8JrQW0U9/B5zeguYdPvYumNmrjkIOeYzVcB/2x0h5I2gH+4chxM11A+oE0RIOCaUZV/iyMRi2et7Xz5300ArdoCdHo9JN4ZoZWa+NikEUqkkbwRis+aK4HXj7qmi6+YiCbtrcmpUVo3lGIl1WVYU1NwvcIet5SzcBG8Rv+AOqNAxwr5Z0aBw/hHpcLPleWCr6kGdc5renjjvbO7HO/MdVY4NF1WtCYL/WOYj8mlz6GsNa8GP1LHcbR8pV/+FGQ5aHBUP4RI1bS+ljoe4wIDAQAB";

        /// <summary>
        /// 实例化RSAHelper
        /// </summary>
        /// <param name="rsaType">加密算法类型 RSA SHA1;RSA2 SHA256 密钥长度至少为2048</param>
        /// <param name="encoding">编码类型</param>
        public RSAHelper(RSAType rsaType = RSAType.RSA, Encoding encoding = null)
        {
            this.encoding = encoding ?? Encoding.UTF8;

            privateKeyRsaProvider = CreateRsaProviderFromPrivateKey(RSA_PRIKEY);
            publicKeyRsaProvider = CreateRsaProviderFromPublicKey(RSA_PUBKEY);

            hashAlgorithmName = rsaType == RSAType.RSA ? HashAlgorithmName.SHA1 : HashAlgorithmName.SHA256;
        }

        #region 加密

        public string Encrypt(string text)
        {
            if (publicKeyRsaProvider == null)
            {
                throw new Exception("publicKeyRsaProvider is null");
            }
            return Convert.ToBase64String(publicKeyRsaProvider.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.Pkcs1));
        }

        #endregion

        #region 解密

        public string Decrypt(string cipherText)
        {
            try
            {
                if (privateKeyRsaProvider == null)
                {
                    throw new Exception("privateKeyRsaProvider is null");
                }
                return Encoding.UTF8.GetString(privateKeyRsaProvider.Decrypt(Convert.FromBase64String(cipherText), RSAEncryptionPadding.Pkcs1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        #endregion 解密

        #region 私钥签名

        /// <summary>
        /// 使用私钥签名
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <returns></returns>
        public string Sign(string data)
        {
            byte[] dataBytes = encoding.GetBytes(data);

            var signatureBytes = privateKeyRsaProvider.SignData(dataBytes, hashAlgorithmName, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signatureBytes);
        }

        #endregion 私钥签名

        #region 公钥验证签名

        /// <summary>
        /// 公钥验证签名
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public bool Verify(string data, string sign)
        {
            byte[] dataBytes = encoding.GetBytes(data);
            byte[] signBytes = Convert.FromBase64String(sign);

            var verify = publicKeyRsaProvider.VerifyData(dataBytes, signBytes, hashAlgorithmName, RSASignaturePadding.Pkcs1);

            return verify;
        }

        #endregion 公钥验证签名

        #region 私钥创建RSA实例

        public RSA CreateRsaProviderFromPrivateKey(string privateKey)
        {
            var privateKeyBits = Convert.FromBase64String(privateKey);

            var rsa = RSA.Create();
            var rsaParameters = new RSAParameters();

            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                rsaParameters.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.D = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.P = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Q = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DP = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DQ = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            rsa.ImportParameters(rsaParameters);
            return rsa;
        }

        #endregion 私钥创建RSA实例

        #region 公钥创建RSA实例

        public RSA CreateRsaProviderFromPublicKey(string publicKeyString)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            var x509Key = Convert.FromBase64String(publicKeyString);

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            using (MemoryStream mem = new MemoryStream(x509Key))
            {
                using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
                {
                    byte bt = 0;
                    ushort twobytes = 0;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    seq = binr.ReadBytes(15);       //read the Sequence OID
                    if (!CompareBytearrays(seq, seqOid))    //make sure Sequence for OID is correct
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x00)     //expect null byte next
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                    int modsize = BitConverter.ToInt32(modint, 0);

                    int firstbyte = binr.PeekChar();
                    if (firstbyte == 0x00)
                    {   //if first byte (highest order) of modulus is zero, don't include it
                        binr.ReadByte();    //skip this null byte
                        modsize -= 1;   //reduce modulus buffer size by 1
                    }

                    byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                        return null;
                    int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                    byte[] exponent = binr.ReadBytes(expbytes);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    var rsa = RSA.Create();
                    RSAParameters rsaKeyInfo = new RSAParameters
                    {
                        Modulus = modulus,
                        Exponent = exponent
                    };
                    rsa.ImportParameters(rsaKeyInfo);

                    return rsa;
                }

            }
        }

        #endregion 公钥创建RSA实例

        #region 导入密钥算法

        private int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
            if (bt == 0x82)
            {
                var highbyte = binr.ReadByte();
                var lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        private bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        #endregion 导入密钥算法

    }

    /// <summary>
    /// RSA算法类型
    /// </summary>
    public enum RSAType
    {
        /// <summary>
        /// SHA1
        /// </summary>
        RSA = 0,
        /// <summary>
        /// RSA2 密钥长度至少为2048
        /// SHA256
        /// </summary>
        RSA2 = 1
    }
}
