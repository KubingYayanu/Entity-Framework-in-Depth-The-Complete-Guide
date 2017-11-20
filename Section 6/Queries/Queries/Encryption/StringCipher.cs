using Queries.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Encryption
{
    public static class StringCipher
    {
        private static string _key = "TasteThisShitYouFuckingMoron";
        private static string _iv = "fm0F2dfSc";

        /// <summary>
        /// 使用AES加密 To HexString
        /// </summary>
        /// <param name="source">本文</param>
        /// <param name="uppercase"></param>
        /// <returns></returns>
        public static string EncryptAES(string source, bool uppercase = false)
        {
            byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
            var aes = new RijndaelManaged();
            var md5 = new MD5CryptoServiceProvider(); // 128bit

            aes.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(_key)); // Min: 128bit、Max: 256bit
            aes.IV = md5.ComputeHash(Encoding.Unicode.GetBytes(_iv)); // Min、Max: 128bit
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = aes.CreateEncryptor();
            
            return transform.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length).ByteArrayToHexString(uppercase);
        }

        /// <summary>
        /// 使用AES解密 From HexString
        /// </summary>
        /// <param name="encryptData">Hex加密後的字串</param>
        /// <returns></returns>
        public static string DecryptAES(string encryptData)
        {
            var encryptBytes = encryptData.HexStringToByteArray();
            var aes = new RijndaelManaged();
            var md5 = new MD5CryptoServiceProvider(); // 128bit

            aes.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(_key)); // Min: 128bit、Max: 256bit
            aes.IV = md5.ComputeHash(Encoding.Unicode.GetBytes(_iv)); // Min、Max: 128bit
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = aes.CreateDecryptor();

            return Encoding.UTF8.GetString(transform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length));
        }
    }
}
