using System.Security.Policy;

namespace Core.Models.Dtos.Responses.Synapse.EmailAndPushNotifications
{
    public class RegisterEmailResponse
    {
        public int Id { get; set; }

        public string FromEmail { get; set; }

        public int Status { get; set; }

        public int CurrentStatus { get; set; }

        public string RejectReason { get; set; }
    }

    public class RegisteremailSaveResponse
    {
        public bool IsValid { get; set; }

        public string Message { get; set; }

        public int CampId { get;set; }
    }


    public class MapRegisterEmailResponse
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int FromEmailId { get; set; }

        public string FromEmail { get; set; }

        public int MailBoxId { get; set; }

        public string MailBox { get; set; }

        public string Status { get; set; }

        public int CurrentStatus { get; set; }

        public string RejectReason { get; set; }
    }

    public class EmailCampaign
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int FromMailId { get; set; }

        public string FromEmail { get; set; }

        public string SecretKeyLabelId { get; set; }

        public string SecretKeyLabel { get; set; }

        public int CAMPAIGNTYPE { get; set; }

        public string CAMPAIGNTYPENAME { get; set; }

        public string MESSAGE { get; set; }

        public int CharacterCount { get; set; }

        public int ScheduledType { get; set; }

        public string Schedule { get; set; }

        public string PlaceHolders { get; set; }

        public string DuplicateRecipients { get; set; }

        public int ChannelType { get; set; }

        public int IsDone { get; set; }

        public int CreatedBy { get; set; }

        /// <summary>
        /// 1 - xls, 2 -xlsx, 3 - csv, 4 - txt
        /// </summary>
        public int RecipientsType { get; set; }

        public string ActualFileName { get; set; }

        public string ImportFileName { get; set; }

        public string GroupIds { get; set; }

        public int ValidCount { get; set; }

        public int CurrentStatus { get; set; }

        public string RecipientField { get; set; }

        public string SheetName { get; set; }

        public string EmailSubject { get; set; }

        public string MessageType { get; set; }

        public int Status { get; set; }

        public int PhoneType { get; set; }
    }
}
