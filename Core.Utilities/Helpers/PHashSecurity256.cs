using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Helpers
{    
    public class PHashSecurity256 : IPHashSecurity256
    {
        public string HashPassword(string password, string username)
        {
            var sha1CryptService = new SHA256Managed();
            var passwordBytes = Encoding.Unicode.GetBytes(password);
            var saltBytes = Encoding.Unicode.GetBytes(username.ToLowerInvariant());
            var secret = new byte[passwordBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, secret, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, secret, passwordBytes.Length, saltBytes.Length);
            var key = sha1CryptService.ComputeHash(secret);
            return Convert.ToBase64String(key);
        }
    }
}
