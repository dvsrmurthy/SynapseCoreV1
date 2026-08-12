using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.EmailToSms
{
    public class GetSMSToEmailRequest
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public string requestedby { get; set; }
        public string UserIp { get; set; }
    }

    public class SaveSMSToEmailRequest 
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public string Originator { get; set; }
        public string Destination { get; set; }
        public string ServerDetails { get; set; }
        public string Port { get; set; }
        public string Encryption { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MoCampaignId { get; set; }
        public string TemplateId { get; set; }
        public int CreatedBy { get; set; }
        public int ReturnValue { get; set; }
        public string command { get; set; }
        public string SMSUserName { get; set; }
        public int Currentstatus { get; set; }
        public string CCEmail { get; set; }
        public string UserIp { get; set; }
    }
    public class GetMOSMSToEmailRequest
    {
        public int UserId { get; set; }
        public int IsSMTPMail { get; set; }
        public string UserIp { get; set; }
    }
}
