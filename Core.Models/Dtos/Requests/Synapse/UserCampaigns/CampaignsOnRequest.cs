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
        public string SearchCampaign { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int ModuleType { get; set; }
        public string UserIp { get; set; }
        public bool ischecker { get; set; }
    }
    public class ShowGridCampaignsOnRequest
    {
        public int CampID { get; set; }
        public string CampStatus { get; set; }
        public int CreatedBy { get; set; }
        public int DisplayIntrvl { get; set; }
    }
    public class RolesPriviligesOnRequest
    {
        public long UserId { get; set; }
        public string ApplicationType { get; set; }
        public int Status { get; set; }
        public string FilterName { get; set; }
    }
    public class LoadCampaignTypeOnRequest
    {
        public long CustomerId { get; set; }
        public int Status { get; set; }
        public int CampaignTypeId { get; set; }
        public string CampaignTypeName { get; set; }
        public int CampaignTypeStatus { get; set; }
        public string PreferredName { get; set; }
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
        public string TEMPID { get; set; }
    }
    public class InsertQSMSOnRequest
    {
        public int QSMSID { get; set; }
        public int SenderID { get; set; }
        public int LangID { get; set; }
        public string Message { get; set; }
        public int CharCount { get; set; }
        public int CreditsUsed { get; set; }
        public int DLR { get; set; }
        public int CreatedBy { get; set; }
        public string Sender { get; set; }
        public string Module { get; set; }
        public string MobileNos { get; set; }
        public int CurrentStatus { get; set; }
        public int Status { get; set; }
        public string Stage { get; set; }
        public int CreditsCount { get; set; }
        public int CustomerID { get; set; }
        public string UserName { get; set; }
        public string UserIp { get; set; }
        public string Sendtime { get; set; }
        public string Medata { get; set; }
        public int UserId { get; set; }
        public string OTPValue { get; set; }
    }
    public class CheckerUpdateQSMSOnRequest
    {
        public int QSMSID { get; set; }
        public int Status { get; set; }
        public int CurrentStatus { get; set; }
        public string RejectReason { get; set; }
        public int UpdatedBy { get; set; }
        public int ModuleType { get; set; }
    }
    public class InsertBulkSMSOnRequest
    {
        public int CustomerID { get; set; }
        public dynamic CampID { get; set; }
        public string Name { get; set; }
        public dynamic SenderID { get; set; }
        public dynamic LangID { get; set; }
        public string Language { get; set; }
        public dynamic CampaignTypeID { get; set; }
        public string CampaignType { get; set; }
        public string Message { get; set; }
        public dynamic CharCount { get; set; }
        public dynamic CreditsUsed { get; set; }
        public dynamic ScheduledType { get; set; }
        public string XMLSchedule { get; set; }
        public string Criteria { get; set; }
        public string PlaceHolders { get; set; }
        public dynamic AllowDuplicates { get; set; }
        public dynamic DLR { get; set; }
        public dynamic MessageType { get; set; }
        public dynamic Status { get; set; }
        public int CreatedBy { get; set; }
        public string IpAddress { get; set; }
        public string Sender { get; set; }
        public string Type { get; set; }//module        
        public int CurrentStatus { get; set; }
        public string ImportFileName { get; set; }
        public string ActualFileName { get; set; }
        public string SheetName { get; set; }
        public int ValidCount { get; set; }
        public int InValidCount { get; set; }
        public int DuplicateCount { get; set; }
        public string DuplicatePath { get; set; }
        public string InvalidMobPath { get; set; }
        public dynamic RecipientsType { get; set; }
        public string MobileField { get; set; }
        public string GroupIds { get; set; }
        public string RuleId { get; set; }
        public string TempTableName { get; set; }
        public string Remarks { get; set; }
        public int IsDone { get; set; }
        public int TotalCount { get; set; }
        public int ProcessedCount { get; set; }
        public DateTime Schedule { get; set; }
        public string TotalScheduleString { get; set; }
        public DateTime PreprocessTime { get; set; }
        public int IsProcess { get; set; }
        public int PreProcessStatus { get; set; }
        public dynamic SchStatus { get; set; }
        public string Stageids { get; set; }
        public int TotalCreditsReq { get; set; }
        public Dictionary<string, string> CountryWiseCnt { get; set; }
        public string UserName { get; set; }
        public string UserIp { get; set; }
        public string GroupOldFilePath { get; set; }
        public string BeforeEditSchTime { get; set; }

        public string IncomeGroup { get; set; }
        public string Nationality { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string CampaignCount { get; set; }
        public string FromRange { get; set; }
        public string ToRange { get; set; }
        public string MessageField { get; set; }
        public string category { get; set; }
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
        public string MobileNo { get; set; }
        public dynamic SmsType { get; set; }
        public string Message { get; set; }
        public dynamic CharCount { get; set; }
        public dynamic Credits { get; set; }
        public dynamic ReferenceId { get; set; }
        public string ShortCode { get; set; }
        public string Sender { get; set; }
        public string OrgAddress { get; set; }
        public string ModuleType { get; set; }
        public string CountryName { get; set; }//module        
        public string ModuleUniqId { get; set; }
        public string SenderName { get; set; }
        public string ModuleName { get; set; }
        public string SmppReferenceId { get; set; }
        public string SmppSenderName { get; set; }
        public dynamic MessageQNo { get; set; }
        public dynamic SmppStage { get; set; }
        public int TotalCredReq { get; set; }
        public dynamic SenderId { get; set; }
        public List<InsertTestSMSOnRequest> InsertTestRecords { get; set; }
        public bool IsCustomTest { get; set; }
        public string UserName { get; set; }
        public string Filepath { get; set; }
        public string SheetName { get; set; }
        public string MobileColumn { get; set; }
        public string msgtemptext { get; set; }
        public string UserIp { get; set; }
        public string GroupIds { get; set; }
        public string CampaignType { get; set; }
        public List<ContactList> ContactList { get; set; }
        public string Sheet { get; set; }
        public string category { get; set; }
    }
    public class ContactList
    {
        public int CreatedBy { get; set; }
        public string GroupName { get; set; }
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public Cstatus Fstatus { get; set; }
    }
    public class GetStageCountsOnRequest
    {
        public int CampID { get; set; }
        public string StageIDs { get; set; }
    }
    public class SetCampEventsOnRequest
    {
        public int CampID { get; set; }
        public string StageIDs { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int CurrentStatus { get; set; }
        public string dirname { get; set; }
        public int tcount { get; set; }
        public string UserIp { get; set; }
    }
    public class ExternalDB
    {
        public string Incomegroup { get; set; }
        public string Nationality { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string CampaignCount { get; set; }
    }
    public class LoadSenderByCategory
    {
        public int userId { get; set; }
        public string category { get; set; }
    }
}
