using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;

namespace Core.Models.Dtos.Responses.Synapse.Customers
{
    public class CustomerAppPreferencesResponseMain
    {
        public List<CustomerAppPreferencesResponse> CustomerAppPreferencesResponse { get; set; }
    }
    public class CustomerAppPreferencesResponse
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }

        public int NumberOfGroups { get; set; }

        public int NumberOfContacts { get; set; }

        public int CreditType { get; set; }

        public bool CampaignReportStatus { get; set; }

        public bool ProcessEmailsSmtpToSms { get; set; }

        public bool AllowTransactionalCampaigns { get; set; }

        public bool ProductLogoReplacedWithCLogo { get; set; }

        public bool IsConsiderMargin { get; set; }

        public int ConsiderMargin { get; set; }

        public int CustomerExpiryDateMargin { get; set; }

        public string? ToEmail { get; set; }

        public string? CcEmail { get; set; }

        public int FilterByColumn { get; set; }

        public int GridSize { get; set; }

        public bool Status { get; set; }

        public int CurrentStatus { get; set; }

        public string? RejectNotes { get; set; }

        public int CreatedBy { get; set; }

        public string? CreatedOn { get; set; }

        public bool ReplaceFilterword { get; set; }

        public string? UserIp { get; set; }
    }


}
