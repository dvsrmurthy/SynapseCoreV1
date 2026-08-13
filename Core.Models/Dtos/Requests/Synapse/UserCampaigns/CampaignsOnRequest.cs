using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserCampaigns
{
    public class ShowGridQuickOnRequest
    {
        public int QuickOrCampaignId { get; set; }
        public int CreatedUserId { get; set; }
        public int Status { get; set; }
        public string? SearchCampaign { get; set; } = string.Empty;
        public string? FromDate { get; set; } = string.Empty;
        public string? ToDate { get; set; } = string.Empty;
        public int ModuleType { get; set; }
        public string? UserIp { get; set; } = string.Empty;
        public bool ischecker { get; set; }
    }
    public class ShowGridCampaignsOnRequest
    {
        public int CampID { get; set; }
        public string? CampStatus { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int DisplayIntrvl { get; set; }
    }
    public class RolesPriviligesOnRequest
    {
        public long UserId { get; set; }
        public string? ApplicationType { get; set; } = string.Empty;
        public int Status { get; set; }
        public string? FilterName { get; set; } = string.Empty;
    }
    public class LoadCampaignTypeOnRequest
    {
        public long CustomerId { get; set; }
        public int Status { get; set; }
        public int CampaignTypeId { get; set; }
        public string? CampaignTypeName { get; set; } = string.Empty;
        public int CampaignTypeStatus { get; set; }
        public string? PreferredName { get; set; } = string.Empty;
    }
    public class LoadSenderIDCampaignsOnRequest
    {
        public int USERID { get; set; }
        public int MODULEID { get; set; }
    }

    public class LoadNationalityCampaignsOnRequest
    {
        public int Id { get; set; }
        public int NationalName { get; set; }
    }

    public class LoadCityCampaignsOnRequest
    {
        public int Id { get; set; }
        public int CityName { get; set; }
    }

    public class LoadIncomegroupCampaignsOnRequest
    {
        public int Id { get; set; }
        public int Incomegroup { get; set; }
    }
    

    public class LoadTemplateCampaignsOnRequest
    {
        public int USERID { get; set; }
        public int CUSTID { get; set; }
        public int CAMPTYPE { get; set; }
        public int STATUS { get; set; }
        public string? TEMPID { get; set; } = string.Empty;
    }
    public class InsertQSMSOnRequest
    {
        public int QSMSID { get; set; }
        public int SenderID { get; set; }
        public int LangID { get; set; }
        public string? Message { get; set; } = string.Empty;
        public int CharCount { get; set; }
        public int CreditsUsed { get; set; }
        public int DLR { get; set; }
        public int CreatedBy { get; set; }
        public string? Sender { get; set; } = string.Empty;
        public string? Module { get; set; } = string.Empty;
        public string? MobileNos { get; set; } = string.Empty;
        public int CurrentStatus { get; set; }
        public int Status { get; set; }
        public string? Stage { get; set; } = string.Empty;
        public int CreditsCount { get; set; }
        public int CustomerID { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? UserIp { get; set; } = string.Empty;
        public string? Sendtime { get; set; } = string.Empty;
        public string? Medata { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string? OTPValue { get; set; } = string.Empty;
    }
    public class CheckerUpdateQSMSOnRequest
    {
        public int QSMSID { get; set; }
        public int Status { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectReason { get; set; } = string.Empty;
        public int UpdatedBy { get; set; }
        public int ModuleType { get; set; }
    }
    public class InsertBulkSMSOnRequest
    {
        public int CustomerID { get; set; }
        public dynamic CampID { get; set; }
        public string? Name { get; set; } = string.Empty;
        public dynamic SenderID { get; set; }
        public dynamic LangID { get; set; }
        public string? Language { get; set; } = string.Empty;
        public dynamic CampaignTypeID { get; set; }
        public string? CampaignType { get; set; } = string.Empty;
        public string? Message { get; set; } = string.Empty;
        public dynamic CharCount { get; set; }
        public dynamic CreditsUsed { get; set; }
        public dynamic ScheduledType { get; set; }
        public string? XMLSchedule { get; set; } = string.Empty;
        public string? Criteria { get; set; } = string.Empty;
        public string? PlaceHolders { get; set; } = string.Empty;
        public dynamic AllowDuplicates { get; set; }
        public dynamic DLR { get; set; }
        public dynamic MessageType { get; set; }
        public dynamic Status { get; set; }
        public int CreatedBy { get; set; }
        public string? IpAddress { get; set; } = string.Empty;
        public string? Sender { get; set; } = string.Empty;
        public string? Type { get; set; } = string.Empty;//module       = string.Empty; 
        public int CurrentStatus { get; set; }
        public string? ImportFileName { get; set; } = string.Empty;
        public string? ActualFileName { get; set; } = string.Empty;
        public string? SheetName { get; set; } = string.Empty;
        public int ValidCount { get; set; }
        public int InValidCount { get; set; }
        public int DuplicateCount { get; set; }
        public string? DuplicatePath { get; set; } = string.Empty;
        public string? InvalidMobPath { get; set; } = string.Empty;
        public dynamic RecipientsType { get; set; }
        public string? MobileField { get; set; } = string.Empty;
        public string? GroupIds { get; set; } = string.Empty;
        public string? RuleId { get; set; } = string.Empty;
        public string? TempTableName { get; set; } = string.Empty;
        public string? Remarks { get; set; } = string.Empty;
        public int IsDone { get; set; }
        public int TotalCount { get; set; }
        public int ProcessedCount { get; set; }
        public DateTime Schedule { get; set; }
        public string? TotalScheduleString { get; set; } = string.Empty;
        public DateTime PreprocessTime { get; set; }
        public int IsProcess { get; set; }
        public int PreProcessStatus { get; set; }
        public dynamic SchStatus { get; set; }
        public string? Stageids { get; set; }
        public int TotalCreditsReq { get; set; }
        public Dictionary<string, string> CountryWiseCnt { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? UserIp { get; set; } = string.Empty;
        public string? GroupOldFilePath { get; set; } = string.Empty;
        public string? BeforeEditSchTime { get; set; } = string.Empty;

        public string? IncomeGroup { get; set; } = string.Empty;
        public string? Nationality { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public string? CampaignCount { get; set; } = string.Empty;
        public string? FromRange { get; set; } = string.Empty;
        public string? ToRange { get; set; } = string.Empty;
        public string? MessageField { get; set; } = string.Empty;
        public string? category { get; set; } = string.Empty;
    }

    public class InsertTestSMSOnRequest
    {
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public int Stage { get; set; }
        public int ProtocolId { get; set; }
        public int SMSCId { get; set; }
        public dynamic ModuleId { get; set; }
        public int LanguageId { get; set; }
        public dynamic MessageId { get; set; }
        public dynamic Priority { get; set; }
        public dynamic SentStatus { get; set; }
        public dynamic CharSet { get; set; }
        public dynamic DLRRequired { get; set; }
        public string? MobileNo { get; set; } = string.Empty;
        public dynamic SmsType { get; set; }
        public string? Message { get; set; } = string.Empty;
        public dynamic CharCount { get; set; }
        public dynamic Credits { get; set; }
        public dynamic ReferenceId { get; set; }
        public string? ShortCode { get; set; } = string.Empty;
        public string? Sender { get; set; } = string.Empty;
        public string? OrgAddress { get; set; } = string.Empty;
        public string? ModuleType { get; set; } = string.Empty;
        public string? CountryName { get; set; } = string.Empty;//module        
        public string? ModuleUniqId { get; set; } = string.Empty;
        public string? SenderName { get; set; } = string.Empty;
        public string? ModuleName { get; set; } = string.Empty;
        public string? SmppReferenceId { get; set; } = string.Empty;
        public string? SmppSenderName { get; set; } = string.Empty;
        public dynamic MessageQNo { get; set; }
        public dynamic SmppStage { get; set; }
        public int TotalCredReq { get; set; }
        public dynamic SenderId { get; set; }
        public List<InsertTestSMSOnRequest> InsertTestRecords { get; set; }
        public bool IsCustomTest { get; set; }
        public string? UserName { get; set; } = string.Empty;        
        public string? Filepath { get; set; } = string.Empty;
        public string? SheetName { get; set; } = string.Empty;
        public string? MobileColumn { get; set; } = string.Empty;
        public string? msgtemptext { get; set; } = string.Empty;
        public string? UserIp { get; set; } = string.Empty;
        public string? GroupIds { get; set; } = string.Empty;
        public string? CampaignType { get; set; } = string.Empty;
        public List<ContactList> ContactList { get; set; }
        public string? Sheet { get; set; } = string.Empty;
        public string? category { get; set; } = string.Empty;
    }
    public class ContactList
    {
        public int CreatedBy { get; set; }
        public string? GroupName { get; set; } = string.Empty;
        public string? MobileNo { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public Cstatus Fstatus { get; set; } 
    }
    public class GetStageCountsOnRequest
    {
        public int CampID { get; set; }
        public string? StageIDs { get; set; } = string.Empty;
    }
    public class SetCampEventsOnRequest
    {
        public int CampID { get; set; }
        public string? StageIDs { get; set; } = string.Empty;
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int CurrentStatus { get; set; }
        public string? dirname { get; set; } = string.Empty;
        public int tcount { get; set; }
        public string? UserIp { get; set; } = string.Empty;
    }
    public class ExternalDB
    {
        public string? Incomegroup { get; set; } = string.Empty;
        public string? Nationality { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public string? CampaignCount { get; set; } = string.Empty;
    }
    public class LoadSenderByCategory
    {
        public int userId { get; set; }
        public string? category { get; set; } = string.Empty;
    }
}
