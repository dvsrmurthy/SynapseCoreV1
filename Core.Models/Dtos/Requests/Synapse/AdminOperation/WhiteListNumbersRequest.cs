using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AdminOperation
{
    public class WhiteListNumbersRequest
    {
        public int nId { get; set; }
        public string? strMobileNo { get; set; }
        public int nCustomerId { get; set; }
        public int nUserId { get; set; }
        public int nSMSCId { get; set; }
        public int nWhiteListId { get; set; }        
        public bool nStatus { get; set; }               
        public string? UserIp { get; set; }
        public string? SearchText { get; set; }
        public int requestedby { get; set; }
    }
}
