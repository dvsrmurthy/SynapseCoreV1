using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserCampaigns
{
    public class ShowGridQuickOnResponse
    {
        public long ID { get; set; }
        public string? Name { get; set; }
        public string? Sender { get; set; }
        public long SenderId { get; set; }
        public int Language { get; set; }
        public string? LanguageName { get; set; }
        public string? Message { get; set; }
        public int CharCount { get; set; }
        public int CreditsUsed { get; set; }
        public int ScheduledType { get; set; }
        public string? Schedule { get; set; }
        public string? Criteria { get; set; }
        public string? PlaceHolders { get; set; }
        public int DuplicateRecipients { get; set; }
        public int Dlr { get; set; }
        public int MessageType { get; set; }
        public int Status { get; set; }
        public long CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
        public string? AddedDate { get; set; }
        public string? ScheduleDate { get; set; }
        public int RecipientsType { get; set; }
        public string? ActualFileName { get; set; }
        public string? ImportFileName { get; set; }
        public string? GroupIds { get; set; }
        public long ValidCount { get; set; }
        public int CurrentStatus { get; set; }
        public string? stageids { get; set; }
        public int CAMPAIGNTYPE { get; set; }
        public int RuleId { get; set; }
        public string? Remarks { get; set; }
        public string? MobileField { get; set; }
        public string? SheetName { get; set; }
        public int ProcessedCount { get; set; }
        public string? PreprocessTime { get; set; }
        public int IsCheckerRequired { get; set; }
        public int CustID { get; set; }
        public string? Category { get; set; }
    }
    public class ShowGridCampaignsOnResponse
    {
        public int CMP_INT_ID { get; set; }
        public string? CMP_VAR_NAME { get; set; }
        public string? CMP_VAR_Field7 { get; set; }
        public int CMP_INT_SID { get; set; }
        public string? SID_VAR_CODE { get; set; }
        public int CMP_INT_LANGUAGE { get; set; }
        public string? LNG_VAR_NAME { get; set; }
        public int CMP_INT_CAMPAIGNTYPE { get; set; }
        public string? CMP_VAR_MESSAGE { get; set; }
        public int CMP_INT_CHARCOUNT { get; set; }
        public int CMP_INT_CREDITSUSED { get; set; }
        public int CMP_INT_SCHTYPE { get; set; }
        public string? CMP_VAR_SCHEDULE { get; set; }
        public string? CMP_VAR_CRITERIA { get; set; }
        public string? CMP_VAR_PLACEHOLDERS { get; set; }
        public int CMP_SINT_DUPRCP { get; set; }
        public int CMP_SINT_DLR { get; set; }
        public int CMP_SINT_MSGTYPE { get; set; }
        public int CMP_SINT_STATUS { get; set; }
        public int CMP_SINT_CREATEDBY { get; set; }
        public string? CMP_DTM_CREATEDON { get; set; }
        public string? CMP_DTM_UPDATEDON { get; set; }
        public string? CMP_DTM_SCHEDULE { get; set; }
        public string? SCHEDULEDATE { get; set; }
        public string? ADDEDDATE { get; set; }
        public string? CMP_VAR_FIELD5 { get; set; }
        public string? CMP_VAR_FIELD6 { get; set; }
        public string? CMP_VAR_Field8 { get; set; }
        public int CPI_INT_ID { get; set; }
        public int CPI_SINT_RCPTYPE { get; set; }
        public string? CPI_VAR_ACTUALFILENAME { get; set; }
        public string? CPI_VAR_IMPFILENAME { get; set; }
        public string? CPI_VAR_GROUPIDS { get; set; }
        public string? CPI_VAR_Field3 { get; set; }
    }
    public class IsUnicodeCharactersFoundOnResponse
    {
        public int GSM_INT_ID { get; set; }
        public string? GSM_VAR_CHARACTER { get; set; }
        public string? GSM_VAR_TYPE { get; set; }
    }
    public class IsUnicodeCharacters
    {
        public string? Characters { get; set; }
        public string? ExctraCharacters { get; set; }
    }
    public class RolesPriviligesOnResponse
    {
        public string? APFTR_VAR_FTR_NAME { get; set; }
        public int ROLPS_INT_FEATURE_ID { get; set; }
    }
    public class CampaignTypesOnResponse
    {
        public int Cmp_int_Id { get; set; }
        public string? Cmp_var_Name { get; set; }
        public int Cmp_Sint_Status { get; set; }
    }
    public class CustomerPreferencesCmpOnResponse
    {
        public int Prf_Int_Id { get; set; }
        public string? Pref_Var_Name { get; set; }
        public string? Pref_Var_Value { get; set; }
        public int Prf_int_Prefid { get; set; }
        public string? Prf_Var_Prefvalue { get; set; }
    }
    public class LoadCampaignTypeOnResponse
    {
        //CampaignTypesOnResponse CampaignType = new CampaignTypesOnResponse();
        public List<CampaignTypesOnResponse> CampaignType { get; set; }
        public long PreferredId { get; set; }
        public string? PreferredValue { get; set; }
    }
    public class LoadSenderIDCampaignsOnResponse
    {
        public int Id { get; set; }
        public string? Code { get; set; }
    }

    public class LoadNationalityCampaignsOnResponse
    {
        public int Id { get; set; }
        public string? NationalName { get; set; }
    }

    public class LoadCityCampaignsOnResponse
    {
        public int Id { get; set; }
        public string? CityName { get; set; }
    }

    public class LoadIncomegroupCampaignsOnResponse
    {
        public int Id { get; set; }
        public string? Incomegroup { get; set; }
    }

    public class LoadTemplateCampaignsOnResponse
    {
        public int TempId { get; set; }
        public string? TemplateName { get; set; }
        public string? Text { get; set; }
        public int Language { get; set; }
        public int Type { get; set; }
        public bool TempStatus { get; set; }
    }
    public class GetgGSMCharsQSMSCampOnResponse
    {
        public int Id { get; set; }
        public string? Char { get; set; }
        public string? Type { get; set; }
    }
    public class GetStageCountsOnResponse
    {
        public int? PauseCnt { get; set; }
        public int? ResumeCnt { get; set; }
    }

    public class DndNonDndNumbers
    {
        public List<strDndNumbers> strDndNumbers;
        public List<strNonDndNumbers> strNonDndNumbers;
    }
    public class strDndNumbers
    {
        public string? DNDNumbers { get; set; }

    }
    public class strNonDndNumbers
    {
        public string? NonDNDNumbers { get; set; }

    }

    public class MobileNos
    {
        public string? MobileNo { get; set; }

    } 

    public class Filedata
    {
        public string? Mobileno { get; set; }
        public string? message { get; set; }
    }
    public class LoadSenderByCategoryResponse
    {
        public int Id { get; set; }
        public string? Code { get; set; }
    }
}
