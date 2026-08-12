using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AdminOperation
{
    public class WhitelistResponse
    {
        public int Id { get; set; }
        public string MobileNo { get; set; }
        public bool Status { get; set; }
    }

    public class ExportWhitelistResponse
    {
        public string MobileNumber { get; set; }
    }
}
