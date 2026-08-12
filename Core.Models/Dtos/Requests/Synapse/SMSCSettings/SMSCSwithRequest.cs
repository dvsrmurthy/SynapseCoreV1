using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    public class SMSCSwithRequest
    {
        public int Id { get; set; }
        public string? FromSMSC { get; set; }
        public string? ToSMSC { get; set; }
        public string? CountryCode { get; set; }
        public string? Country { get; set; }
        public string? Sender { get; set; }
        public string? UserId { get; set; }
        public int CreatedBy { get; set; }
        public bool Status { get; set; }
        public string? requestedby { get; set; }
        public string? UserIp { get; set; }
        public string? Routes { get; set; }
    }
}
