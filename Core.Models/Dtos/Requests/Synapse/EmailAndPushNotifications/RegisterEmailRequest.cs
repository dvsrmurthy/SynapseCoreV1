using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.EmailAndPushNotifications
{
    public class RegisterEmailRequest
    {
        public int Id { get; set; }

        public string FromEmail { get; set; }

        public int Status { get; set; }

        public int CreatedBy { get; set; }

        public int CurrentStatus { get; set; }

        public string RejectReason { get; set; }

        public string UserIp { get; set; }

        public int IsStatusUpdate { get; set; }
    }

    public class MapRegisterEmailRequest
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int UserId { get; set; }

        public int RegisterMailId { get; set; }

        public int MailBoxId { get; set; }

        public int CreatedBy { get; set; }

        public int Status { get; set; }

        public int CurrentStatus { get; set; }

        public string RejectReason { get; set; }

        public string UserIp { get; set; }

        public int IsStatusUpdate { get; set; }
    }

    public class EmailCampaignPDb
    {
        public int Id { get; set; }

        public string CampaignName { get; set; }

        public int RecipientType { get; set; }

        public string FileName { get; set; }

        public string ActualFileName { get; set; }

        public string Group { get; set; }

        public int SecretKey { get; set; }

        public string FromMailId { get; set; }

        public string FromMail { get; set; }

        public string EmailSubject { get; set; }

        public int MessageType { get; set; }

        public string MessageBody { get; set; }

        public int ScheduleType { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string StartTime { get; set; }

        public string StartMinutes { get; set; }

        public string RecipientName { get; set; }

        public string Date { get; set; }

        public int Status { get; set; }

        public int ChannelType { get; set; }

        public int CurrentStatus { get; set; }

        public string RejectReason { get; set; }

        public int CreatedBy { get; set; }

        public int CustomerId { get; set; }

        public int UserId { get; set; }

        public string UserIp { get; set; }

        public int TotalCount { get; set; }

        public int ValidCount { get; set; }

        public int InvalidCount { get; set; }

        public int DuplicateCount { get; set; }

        public string ColumnName { get; set; }

        public int PhoneType { get; set; }

        public int IsTestepn { get; set; }
    }

    public class EmailCampaignRequest
    {
        public int CampaignId { get; set; }

        public int AddedBy { get; set; }

        public string CampaignName { get; set; }

        public string EmailFrom { get; set; }

        public string EmailTo { get; set; }

        public int ChannelType { get; set; }

        public string UserIp { get; set; }

        public int nReturn { get; set; }
    }
}