using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.MailBox
{
    public class UserMailBoxMappingResponse
    {
        public int Id { get; set; }
        public string Mailbox { get; set; }
        public string Name { get; set; } //CustomerName
        public string UserName { get; set; }
        public string Code { get; set; }
        public int MailType { get; set; }

        public int CurrentStatus { get; set; }
        public int Status { get; set; }
        public bool BIDInvitation { get; set; }
        public bool Recipients { get; set; }
        public bool Attachment { get; set; }
        public bool GroupName { get; set; }
        public bool MobileNoDomain { get; set; }
        public bool DRR { get; set; }
        public bool Tags { get; set; }
        public string STags { get; set; }
        public string ETags { get; set; }


        //---Inserting---
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int MbcId { get; set; }
        public int Language { get; set; }
        public string MobilityName { get; set; }
        public int SenderId { get; set; }
        public int MailboxId { get; set; }
        public string FormateIds { get; set; }
        public string AuthorRequired { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int DlrRequired { get; set; }
        public int LimitedCredits { get; set; }
        public int MaxCredits { get; set; }
        public int LimitRecepients { get; set; }
        public int MaxRecepients { get; set; }
        public int RecepientType { get; set; }
        public int LimitNotification { get; set; }
        public int MaxNotification { get; set; }
        public int NotificationDuration { get; set; }
        public int NoofRetries { get; set; }
        public bool UseTags { get; set; }
        public string StartTags { get; set; }
        public string EndTags { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
    }

    public class UserbyCustomerIdRes
    {
        public string UserName { get; set; }
    }
    public class SenderbyUserIdRes
    {
        public int Id { get; set; }
        public string Code { get; set; }
    }
}
