using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserMoCampaignConfiguration
{
    #region MoCampaign
    public class MoCampaignOnResponse
    {

        public string? Customer { get; set; }
        public int CustomerID { get; set; }

        public string? User { get; set; }
        public int UserID { get; set; }

        public string? CampaignName { get; set; }
        public int MOCampaignID { get; set; }

        public string? Description { get; set; }
        public string? ShortCodeType { get; set; }
        public int ShortCType { get; set; }

        public string? ShortCode { get; set; }
        public int ShCode { get; set; }
        public string? SenderID { get; set; }

        public string? Keyword { get; set; }
        public int intKwrd { get; set; }

        public string? SubKeyword { get; set; }
        public int intSbKwd { get; set; }

        public string? Outbound { get; set; }
        public int intOutBnd { get; set; }

        public string? SystemID { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

        public int LangSelect { get; set; }
        public string? Language { get; set; }
        public string? KeywordName { get; set; }
        public string? ReplyMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public int ForwardType { get; set; }
        public string? HTTPType { get; set; }
        public string? SMPPType { get; set; }
        public string? CustomType { get; set; }

        public string? URL { get; set; }
        public string? MobileNumber { get; set; }
        public string? MobileNumberOrder { get; set; }
        public string? Destination { get; set; }
        public string? DestinationOrder { get; set; }
        public string? Message { get; set; }
        public string? MessageOrder { get; set; }
        public string? DateTime { get; set; }
        public string? DateTimeOrder { get; set; }
        public string? Others { get; set; }
        public string? OthersOrder { get; set; }
        public string? Value { get; set; }
        public string? RetryHour { get; set; }

        public string? RetryMinute { get; set; }
        public string? RetrySMPPHour { get; set; }
        public string? RetrySMPPMinute { get; set; }
        public string? SMPPAccount { get; set; }
        public string? Component { get; set; }
        public string? Parameter { get; set; }
        public bool Status { get; set; }

        public int CurrentStatus { get; set; }
        public string? RejectReason { get; set; }
        public string? command { get; set; }
        public string? SMPPUser { get; set; }

        public int AddedUpdaeBy { get; set; }
        public string? requestedby { get; set; }
        public int MOC_INT_CAMPID { get; set; }
        public string? MOC_VAR_CAMPNAME { get; set; }
        public string? MOC_VAR_DESC { get; set; }
        public string? MOC_VAR_SCHEDULE { get; set; }
        public string? MOC_INT_SHORTCODE { get; set; }
        public int MOC_INT_KEYWORD { get; set; }
        public int MOC_INT_SYSTEMID { get; set; }
        public string? MOC_INT_STATUS { get; set; }
        public int MOC_INT_USERID { get; set; }
    }
    public class GetMoCampaignNamesOnResponse
    {
        public int MOC_INT_CAMPID { get; set; }
        public int MOC_VAR_CAMPNAME { get; set; }
        public int MOC_VAR_DESC { get; set; }
        public int MOC_VAR_SCHEDULE { get; set; }
    }
    public class MoCompaginsForXMLOnResponse
    {

        public int nMOCINTCAMPID { get; set; }
        public string? strMOCVARCAMPNAME { get; set; }
        public string? strMOCVARDESC { get; set; }
        public int nMOCVARSHORTCODE { get; set; }
        public int nMOCVARKEYWORD { get; set; }
        public int nMOCVARSCHEDULE { get; set; }
        public int nMOCINTSTATUS { get; set; }
        public int nMOCINTUSERID { get; set; }
        public string? strMOCDETCREATEDDATE { get; set; }
        public int nMOCINTSHORTCODE { get; set; }
        public int nRetVal { get; set; }
        public string? strStartime { get; set; }
        public string? strEndTime { get; set; }
        public int nInterval { get; set; }
        public string? strDate { get; set; }
        public string? strEndOn { get; set; }
        public string? strAlertOn { get; set; }
        public int nWeek { get; set; }
        public string? strWeekdays { get; set; }
        public string? strDays { get; set; }
        public string? strMonths { get; set; }
        public int nEndOn { get; set; }
        public string? strEndDate { get; set; }
        public string? strStartDate { get; set; }
        public string? strSchedulexml { get; set; }
        public int nCUSTOMERID { get; set; }
        public int nCreatedBy { get; set; }
        public string? nMOCINTSYSTEMID { get; set; }
        public string? strIds { get; set; }
        public string? strSearch { get; set; }
    }

    public class MoCampaignSearchResponse
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? UserName { get; set; }
    }
    #endregion
    #region MoReply
    public class MoReply
    {
        public int MRP_INT_ID { get; set; }
        public int MRP_INT_CAMPID { get; set; }
        public int MRP_INT_REPLYTYPE { get; set; }
        public int MRP_INT_LANGUAGE { get; set; }
        public string? LNG_VAR_NAME { get; set; }
        public string? MRP_VAR_REPLYTEXT { get; set; }
        public string? MOC_VAR_CAMPNAME { get; set; }
        public int MRP_SINT_CREATEDBY { get; set; }
        public DateTime MRP_DTM_CREATEDON { get; set; }
        public int MRP_SINT_UPDATEDBY { get; set; }
        public DateTime MRP_DTM_UPDATEDON { get; set; }
        public string? CampaignName { get; set; }
        public string? ReplyType { get; set; }
        public string? Language { get; set; }
        public string? ReplyText { get; set; }
    }

    #endregion
    #region Moforward
    public class MoforwardOnResponse
    {
        public int MFD_INT_ID { get; set; }
        public int MFD_INT_CAMPID { get; set; }
        public string? MOC_VAR_CAMPNAME { get; set; }
        public int MFD_INT_FORWARDTYPE { get; set; }
        public string? MFD_VAR_URL { get; set; }
        public DateTime MFD_DTM_CREATEDON { get; set; }
        public int MFD_SINT_UPDATEDBY { get; set; }
        public DateTime MFD_DTM_UPDATEDON { get; set; }
        public string? MFD_VAR_FIELD1 { get; set; }
        public string? MFD_VAR_FIELD2 { get; set; }
        public string? MFD_VAR_FIELD3 { get; set; }
        public string? CampaignName { get; set;}
        public string? ForwardType { get; set; }
        public string? forwardURL { get; set; }
        public string? RetryType { get; set; }
        public string? RetryAttempts { get; set; }
        public string? RetryInterval { get; set; }
    }
    public class Mosmpp
    {
        public string? ShortCode { get; set; }
        public string? RequiredSMPPForward { get; set; }
        public string? Status { get; set; }
        public string? ChangeStatus { get; set; }
    }
    public class DeleteMoForwardOnresponse
    {
        public int MOF_INT_CAMPID { get; set; }
        public string? MOF_VAR_PARAMNAME { get; set; }
        public string? MOF_VAR_VALUE { get; set; }
        public int MOF_SINT_CREATEDBY { get; set; }
        public DateTime MOF_DTM_CREATEDON { get; set; }
    }

    #endregion
    #region MoSmppForward
    public class MoSmppForwardOnResponse
    {
        public int MSFD_INT_ID { get; set; }
        public int MSFD_INT_SHORTCODE { get; set; }
        public int MSFD_INT_USERID { get; set; }
        public int MSFD_INT_CUSTOMERID { get; set; }
        public int MSFD_INT_REQUIREDSMPPFWD { get; set; }
        public int MSFD_INT_STATUS { get; set; }
        public int MSFD_INT_CREATEDBY { get; set; }
        public DateTime MSFD_DTM_CREATEDON { get; set; }
        public int MSFD_INT_UPDATEDBY { get; set; }
        public DateTime MSFD_DTM_UPDATEDON { get; set; }
    }

    #endregion
    #region Load Methods
    public class LoadSenderIDsOnResponse
    {
        public int SID_INT_ID { get; set; }
        public int SID_INT_TYPE { get; set; }
        public int SID_VAR_CODE { get; set; }
    }

    public class LoadSMSCSOnResponse
    {
        public int SCN_INT_ID { get; set; }
        public int SCN_VAR_USERID { get; set; }
        public string? SHORTCODE { get; set; }
    }

    public class LoadKeywordsOnResponse
    {
        public int KYW_INT_ID { get; set; }
        public string? KYD_VAR_NAME { get; set; }
        public string? KYD_VAR_DESC { get; set; }
        public DateTime KYD_DTM_VALID_FROM { get; set; }
        public DateTime KYD_DTM_VALID_TO { get; set; }
        public int KYD_INT_STATUS { get; set; }
        public int KYD_INT_LANG { get; set; }
    }
    #endregion
}
