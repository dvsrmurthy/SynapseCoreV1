using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.EmailToSms
{
    public class GetSMSToEmailResponse
    {
        public int ID { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int MoCampaignId { get; set; }
        public int TemplateId { get; set; }
        public string Customer { get; set; }
        public string UserName { get; set; }
        public string Port { get; set; }
        public int Encryption { get; set; }
        public string SMSUserName { get; set; }
        public string Originator { get; set; }
        public string Server { get; set; }
        public string MoCampaign { get; set; }
        public bool Status { get; set; }
        public string Destination { get; set; }
        public string EmailTemplate { get; set; }
        public int SenderId { get; set; }
        public string Password { get; set; }
        public string CCEmail { get; set; }
        public string MailBox { get; set; }
        //public int TemplateId { get; set; }
       // public int MoCampaignId { get; set; }
    }

    public class SaveSMSToEmailResponse
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Originator { get; set; }
        public string Destination { get; set; }
        public string ServerDetails { get; set; }
        public string Port { get; set; }
        public int Encryption { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int MoCampaignId { get; set; }
        public int TemplateId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
    public class GetMOSMSToEmailResponse
    {
        public int Id { get; set; }
        public string CampaignName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Mailbox { get; set; }
    }
}
