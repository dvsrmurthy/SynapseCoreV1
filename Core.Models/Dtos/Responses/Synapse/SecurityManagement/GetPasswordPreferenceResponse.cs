using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SecurityManagement
{
    public class GetPasswordPreferenceResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int AlphabetCount { get; set; }
        public int DigitCount { get; set; }
        public int Expiry { get; set; }
        public int History { get; set; }
        public string? StopCharacters { get; set; }
        public int MinLength { get; set; }
        public int Length { get; set; }
        public int UnsuccessfullLoginAttempts { get; set; }
        public bool ChangeOnFirstLogin { get; set; }


        public string? NoOfAttempts { get; set; }
        public string? OTPExpiryMinutes { get; set; }
        public string? FreezeTimeMinutes { get; set; }
    }
}
