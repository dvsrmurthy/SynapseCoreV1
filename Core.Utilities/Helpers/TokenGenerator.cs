using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace Core.Utilities.Helpers
{
    public static class TokenGenerator
    {
        static char[] sDataChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string Generate(int size = 12)
        {
            if (size <= 4) size = 12;
            byte[] data = new byte[1];
            var generator = new RNGCryptoServiceProvider();
            generator.GetNonZeroBytes(data);
            data = new byte[size - 4];
            generator.GetNonZeroBytes(data);
            var result = new StringBuilder();
            return result.Append(data.Select(x => sDataChars[x % (sDataChars.Length)]).ToArray()).Append(GetSalt()).ToString().ToUpper();
        }

        private static string GetSalt()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 4);
        }
    }
}
