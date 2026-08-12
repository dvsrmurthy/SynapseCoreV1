using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    public class TestSMSCResponse
    {
        public string? Createdon { get; set;}
        public int ID { get; set; }
        public int SenderId { get; set; }
        public string? Sender { get; set; }
        public int CustomerId { get; set; }
        public string? Languagename { get; set; }
        public string? Message { get; set; }
        public int CharCount { get; set; }
        public int CreditsUsed { get; set; }
        public bool Dlr { get; set; }
        public int CreatedBy { get; set; }
        public string? SentDate { get; set; }
        public int Status { get; set; }
        public string? Stage { get; set; }
      
        public string? Module { get; set; }
        public string? UserName { get; set; }
        public string? NAME { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? userlist { get; set; }
        public int CurrentStatus { get; set; }
        public int Fstatus { get; set; }
    }
}
