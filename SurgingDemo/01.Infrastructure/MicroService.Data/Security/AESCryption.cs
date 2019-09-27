using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MicroService.Data.Security
{
    public class AESCryption
    {
        public const string Salt = @"5b37977efc6a44bf8102c2fa90a17f1b";

        /// <summary>  
        /// AES加密(无向量)  
        /// </summary>  
        /// <param name="plainBytes">被加密的明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns>密文</returns>  
        public static string EncryptText(String Data, String Key)
        {
            MemoryStream mStream = new MemoryStream();
            RijndaelManaged aes = new RijndaelManaged();

            byte[] plainBytes = Encoding.UTF8.GetBytes(Data);
            Byte[] bKey = new Byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);

            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            //aes.Key = _key;  
            aes.Key = bKey;
            //aes.IV = _iV;  
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }


        /// <summary>  
        /// AES解密(无向量)  
        /// </summary>  
        /// <param name="encryptedBytes">被加密的明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns>明文</returns>  
        public static string DecryptText(String Data, String Key)
        {

            try
            {
                Byte[] encryptedBytes = Convert.FromBase64String(Data);
                Byte[] bKey = new Byte[32];
                Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);

                using (MemoryStream mStream = new MemoryStream(encryptedBytes))
                {
                    //mStream.Write( encryptedBytes, 0, encryptedBytes.Length );  
                    //mStream.Seek( 0, SeekOrigin.Begin );  
                    RijndaelManaged aes = new RijndaelManaged();
                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.KeySize = 128;
                    aes.Key = bKey;
                    //aes.IV = _iV;  
                    using (CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] tmp = new byte[encryptedBytes.Length + 32];
                        int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
                        byte[] ret = new byte[len];
                        Array.Copy(tmp, 0, ret, 0, len);
                        return Encoding.UTF8.GetString(ret);
                    }
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
