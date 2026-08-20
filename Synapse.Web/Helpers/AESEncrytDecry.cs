using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Synapse.Web.Helpers
{
    public static class AESEncrytDecry
    {
        private static IConfiguration? _configuration;

        /// <summary>
        /// Initialize AES configuration.
        /// Call this once from Program.cs.
        /// </summary>
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Gets the AES key from appsettings.json
        /// </summary>
        private static byte[] GetKey()
        {
            if (_configuration == null)
                throw new InvalidOperationException(
                    "AESEncrytDecry has not been initialized.");

            string? scKey = _configuration["ScKey"];
            if(scKey == null)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) // Sets look-up folder to application directory
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
                scKey = configuration["ScKey"];
            }

            if (string.IsNullOrWhiteSpace(scKey))
                throw new InvalidOperationException(
                    "ScKey is missing from appsettings.json.");

            byte[] key = Encoding.UTF8.GetBytes(scKey);

            // AES supports 128, 192 or 256-bit keys
            if (key.Length != 16 &&
                key.Length != 24 &&
                key.Length != 32)
            {
                throw new InvalidOperationException(
                    $"ScKey must be 16, 24 or 32 bytes when UTF-8 encoded. " +
                    $"Current length: {key.Length} bytes.");
            }

            return key;
        }

        /// <summary>
        /// Decrypt an AES encrypted Base64 string.
        /// Compatible with the existing .NET Framework implementation.
        /// </summary>
        public static string DecryptStringAES(string cipherText)
        {
            if (string.IsNullOrWhiteSpace(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            byte[] key = GetKey();

            // IMPORTANT:
            // Existing .NET 4.5 code uses the same ScKey as IV.
            // Keep this unchanged for backward compatibility.
            byte[] iv = GetKey();

            try
            {
                byte[] encryptedBytes =
                    Convert.FromBase64String(cipherText);

                return DecryptStringFromBytes(
                    encryptedBytes,
                    key,
                    iv);
            }
            catch (FormatException)
            {
                throw new CryptographicException(
                    "The supplied cipher text is not a valid Base64 string.");
            }
            catch (CryptographicException)
            {
                throw new CryptographicException(
                    "Unable to decrypt the value. " +
                    "Please verify that ScKey, encryption mode, padding " +
                    "and IV are identical to the original .NET Framework application.");
            }
        }

        private static string DecryptStringFromBytes(
            byte[] cipherText,
            byte[] key,
            byte[] iv)
        {
            if (cipherText == null || cipherText.Length == 0)
                throw new ArgumentNullException(nameof(cipherText));

            if (key == null || key.Length == 0)
                throw new ArgumentNullException(nameof(key));

            if (iv == null || iv.Length == 0)
                throw new ArgumentNullException(nameof(iv));

            using Aes aes = Aes.Create();

            // These settings MUST remain the same as the old application.
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.BlockSize = 128;

            aes.Key = key;
            aes.IV = iv;

            using ICryptoTransform decryptor = aes.CreateDecryptor();

            using MemoryStream msDecrypt =
                new MemoryStream(cipherText);

            using CryptoStream csDecrypt =
                new CryptoStream(
                    msDecrypt,
                    decryptor,
                    CryptoStreamMode.Read);

            using StreamReader srDecrypt =
                new StreamReader(
                    csDecrypt,
                    Encoding.UTF8);

            return srDecrypt.ReadToEnd();
        }

        /// <summary>
        /// Encrypt plain text using the same AES configuration.
        /// </summary>
        public static string EncryptStringAES(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            byte[] key = GetKey();

            // Keep same IV behavior as existing application
            byte[] iv = GetKey();

            byte[] encryptedBytes =
                EncryptStringToBytes(
                    plainText,
                    key,
                    iv);

            return Convert.ToBase64String(encryptedBytes);
        }

        private static byte[] EncryptStringToBytes(
            string plainText,
            byte[] key,
            byte[] iv)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            if (key == null || key.Length == 0)
                throw new ArgumentNullException(nameof(key));

            if (iv == null || iv.Length == 0)
                throw new ArgumentNullException(nameof(iv));

            using Aes aes = Aes.Create();

            // Same settings as .NET Framework version
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.BlockSize = 128;

            aes.Key = key;
            aes.IV = iv;

            using MemoryStream msEncrypt =
                new MemoryStream();

            using CryptoStream csEncrypt =
                new CryptoStream(
                    msEncrypt,
                    aes.CreateEncryptor(),
                    CryptoStreamMode.Write);

            using StreamWriter swEncrypt =
                new StreamWriter(
                    csEncrypt,
                    Encoding.UTF8);

            swEncrypt.Write(plainText);
            swEncrypt.Flush();

            // Important: finish the CryptoStream so PKCS7 padding
            // is written before reading the MemoryStream.
            csEncrypt.FlushFinalBlock();

            return msEncrypt.ToArray();
        }
    }
}