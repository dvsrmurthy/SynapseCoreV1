using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AdminOperation
{
    public class ChangePasswordResponse
    {
        public int AlphabetCount { get; set; }
        public int DigitCount { get; set; }
        public string? StopCharacters { get; set; }
        public int MinLength { get; set; }
        public int Length { get; set; }
    }
}
