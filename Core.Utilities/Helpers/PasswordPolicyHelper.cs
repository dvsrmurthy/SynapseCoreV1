using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Core.Utilities.Helpers
{
    public class PasswordPolicyHelper
    {
        public int MinLength { get; set; }

        public int MaxLength { get; set; }

        public int MinNoOfAlphaB { get; set; }

        public int MinNoOfDigits { get; set; }

        public int DaysToExpire { get; set; }

        public int PasswordRepeat { get; set; }

        public char[] AllowedSpecChars { get; set; }

        public PasswordPolicyHelper(int minLength, int maxLength, int minNoOfAlphaB, int minNoOfDigits, int daysToExpire,
            int passwordRepeat, char[] allowedSpecChars)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            MinNoOfAlphaB = minNoOfAlphaB;
            MinNoOfDigits = minNoOfDigits;
            DaysToExpire = daysToExpire;
            PasswordRepeat = passwordRepeat;
            AllowedSpecChars = allowedSpecChars;
        }

        public bool IsHavingMinLength(string source)
        {
            return source.Length >= MinLength;
        }

        public bool IsHavingMaxLength(string source)
        {
            return source.Length <= MaxLength;
        }

        public bool IsValidMinNoOfAlphaB(string source)
        {            
            return source.Count(char.IsUpper) >= MinNoOfAlphaB;
        }

        public bool IsValidMinNoOfDigits(string source)
        {
            return source.Count(char.IsDigit) >= MinNoOfDigits;
        }

        public bool ValidateSpecialChars(string source)
        {
            var filteredChars = source.Where(character => !char.IsLetterOrDigit(character)).ToList();
            return AllowedSpecChars.Where(a => filteredChars.Any(x => x == a)).Count() == filteredChars.Count;
        }
    }
}
