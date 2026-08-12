using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SecurityManagement
{
    public class PasswordPreferenceResponse
    {
        public int Type { get; set; }
        public string? Code { get; set; }
    }
}
