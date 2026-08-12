using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    public class SenderConfigurationRequest
    {
        public int NID { get; set; }
        public string? Customer { get; set; }
        public int CustomerId { get; set; }
        public string? User { get; set; }
        public int UserId { get; set; }
        public string? SenderId { get; set; }

        public bool SMSChannel { get; set; }
        public bool EmailChannel { get; set; }
        public string? CardBin { get; set; }
        public string? Module { get; set; }

        public int Status { get; set; }
        public int CurrentStatus { get; set; }
        public int NCREATEDBY { get; set; }
        public int NUpdatedBY { get; set; }
        public int NRETURN { get; set; }
        public int BankId { get; set; }
        public string? UserIp { get; set; }
    }

    public class GetSenderConfigurationDetailsRequest
    {
        public int SenderConfigId { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int NUPDATEDBY { get; set; }
        public string? NUPDATEDUSER { get; set; }
        public bool smschan { get; set; }
        public bool echan { get; set; }
        public int Return { get; set; }
        public string? RequestPage { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int SenderId { get; set; }
        public string? UserIp { get; set; }
        //public int BankId { get; set; }
    }
}
