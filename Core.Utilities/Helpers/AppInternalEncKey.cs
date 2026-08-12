using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Helpers
{
    public static class AppInternalEncKey
    {

        private static IConfiguration? _configuration;
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }
        public static RijndaelManaged GetRijndaelManaged(string secretKey)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
        }

        public static byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public static byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }
        // Encrypts plaintext using AES 128bit key and a Chain Block Cipher and returns a base64 encoded string
        public static string Encrypt(string plainText, bool useHexEncoding)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(_configuration["AppInternal"])));
        }
        public static string Decrypt(string cipherText, bool useHexEncoding)
        {
            var encryptedBytes = Convert.FromBase64String(cipherText);
            return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(_configuration["AppInternal"])));
        }
    }
}
