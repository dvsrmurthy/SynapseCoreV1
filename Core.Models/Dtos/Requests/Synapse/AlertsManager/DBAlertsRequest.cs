using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AlertsManager
{
    #region OnlineAlerts
    public class DBAlertsRequest
    {
        public int NID { get; set; }
        public int NUSERID { get; set; }
        public int NSTATUS { get; set; }
        public int NRETURN { get; set; }
        public int ONOROFF { get; set; }
        public int customerid { get; set; }
        public string Requestby { get; set; }
        public string UserIp { get; set; }
    }

    public class SetOnlineAlertsRequest
    {
        public int NID { get; set; }
        public string SERVERIP { get; set; }
        public string STRALERTNAME { get; set; }
        public string STRDESCRIPTION { get; set; }
        public int NSENDERID { get; set; }
        public int NRULEID { get; set; }
        public string STRMOBILEFLD { get; set; }
        public string STRMESSAGEFIELD { get; set; }
        public int NMESSAGETEMPLATEID { get; set; }
        public string STRMESSAGETEMPLATE { get; set; }
        public string STRPLACEHOLDERS { get; set; }
        public string STRMESSAGE { get; set; }
        public string STRCUSTOMMSG { get; set; }
        public bool NDLRREQUIRED { get; set; }
        public string STRSCHEDULE { get; set; }
        public int NCREATEDBY { get; set; }
        public string NCREATEDBYUSER { get; set; }
        public int NUpdatedBY { get; set; }
        public string NUpdatedBYUSER { get; set; }
        public string STRFIELD1 { get; set; }
        public string STRFIELD2 { get; set; }
        public string STRFIELD3 { get; set; }
        public int NRETURN { get; set; }
        public int Language { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }

        public string command { get; set; }
        public int NALERTID { get; set; }
        public int NUPDATEDBY { get; set; }
        public int CURRENTSTATUS { get; set; }

        public int IntervalType { get; set; }
        public string IntervalValue { get; set; }
        public int SchEvrHour { get; set; }
        public int SchEvrMinute { get; set; }
        public int SchEvrSecond { get; set; }
        public string SchEvrDay { get; set; }
        public string SchEvrWeek { get; set; }
        public string SchEvrWeekTime { get; set; }
        public int SendNowOrScheduleLater { get; set; }
        public int STATUS { get; set; }
        public string EmailField { get; set; }
        public int EmailTemplateId { get; set; }
        public string EmailTemplateText { get; set; }
        public string UserIp { get; set; }
    }

    public class GetBusinessRulesRequest
    {
        public int NRuleId { get; set; }
        public int nStatus { get; set; }
        public int nCreatedBy { get; set; }
        public int nONOROFF { get; set; }
        public int NRETVAL { get; set; }
        public int DBType { get; set; }
        public string ConnectionString { get; set; }
        public string Statement { get; set; }
        public string MessageContent { get; set; }
    }

    public class GetSenderRequest
    {
        public int nUserId { get; set; }
        public string requestedby { get; set; }
        public int NRETVAL { get; set; }
    }

    public class GetTemplatesRequest
    {
        public int nTempId { get; set; }
        public int nStatus { get; set; }
        public int nUserId { get; set; }
        public string requestedby { get; set; }
        public int NRETVAL { get; set; }
        public int customer { get; set; }
        public int TEMPLATETYPE { get; set; }
        public string UserIp{get;set;}
    }

    public class ChangeOnlineAlertsStatusRequest
    {
        public int NID { get; set; }
        public int NSTATUS { get; set; }
        public int NUPDATEDBY { get; set; }
        public string NUPDATEDUSER { get; set; }
        public int NRETURN { get; set; }
        public int CURRENTSTATUS { get; set; }
        public string UserIp { get; set; }
    }

    public class GetOnlineAlertsDetailsRequest
    {
        public int Alertid { get; set; }
        public int Status { get; set; }
        public int CreatedByUser { get; set; }
        public int Return { get; set; }
        public string RequestPage { get; set; }
        public int ONOROFF { get; set; }
        public int customerid { get; set; }
        public string UserIp { get; set; }
    }

    public class ApproveRejectAlertsCreation
    {
        public int ALERTId { get; set; }
        public int CURRENTSTATUS { get; set; }
        public int UpdatedBy { get; set; }
        public string Rejectreason { get; set; }
        public int ReturnValue { get; set; }
    }
#endregion

# region OfflineAlerts
    public class GetOfflineAlertsRequest
    {
        public int NALERTID { get; set; }
        public int NUSERID { get; set; }
        public int NSTATUS { get; set; }
        public int NRETURN { get; set; }
        public int customerid { get; set; }
        public string  requestedby { get; set; }
        public string UserIp { get; set; }
    }

    public class ApproveRejectOfflineAlerts
    {
        public int AlertId { get; set; }
        public int CURRENTSTATUS { get; set; }
        public int UpdatedBy { get; set; }
        public string Rejectreason { get; set; }
        public int ReturnValue { get; set; }
    }

    public class GetBusinessOfflineRulesRequest
    {
        public int NRuleId { get; set; }
        public int nStatus { get; set; }
        public int nCreatedBy { get; set; }
        public int NRETVAL { get; set; }
        public int onoroff { get; set; }
        
    }

    public class SetOfflineAlertsRequest
    {
        public int NID { get; set; }
        public string SERVERIP { get; set; }
        public string STRALERTNAME { get; set; }
        public string STRDESCRIPTION { get; set; }
        public int NSENDERID { get; set; }
        public int NRULEID { get; set; }
        public string STRMOBILEFLD { get; set; }
        public string STRMESSAGEFIELD { get; set; }
        public int NMESSAGETEMPLATEID { get; set; }
        public string STRMESSAGETEMPLATE { get; set; }
        public string STRPLACEHOLDERS { get; set; }
        public string STRMESSAGE { get; set; }
        public string STRCUSTOMMSG { get; set; }
        public int NDLRREQUIRED { get; set; }
        public string STRSCHEDULE { get; set; }
        public int NCREATEDBY { get; set; }
        public string NCREATEDBYUSER { get; set; }
        public int NUpdatedBY { get; set; }
        public string NUpdatedBYUSER { get; set; }
        public string EmailField { get; set; }
        public int EmailTemplateId { get; set; }
        public string EmailTemplateText { get; set; }
       
        //public string HOURS { get; set; }
        //public string MINUTES { get; set; }
        //public string STARTDATE { get; set; }
        //public string TIME { get; set; }
       
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public int NRETURN { get; set; }
        public int Language { get; set; }
        public int campaigntype { get; set; }
        public string command { get; set; }
        public int NALERTID { get; set; }
        public int NUPDATEDBY { get; set; }
        public int CURRENTSTATUS { get; set; }

        public int SendNowOrScheduleLater { get; set; }
        
        public int IntervalType { get; set; }
        public string InterValValue { get; set; }
        public int BankId { get; set; }
        public int SchEvrHour { get; set; }
        public int SchEvrMinute { get; set; }
        public int SchEvrSecond { get; set; }
        public string SchEvrDay { get; set; }
        public string SchEvrWeek { get; set; }
        public string SchEvrWeekTime { get; set; }
      
       
        public int IsResponseEmailReq { get; set; }
        public int CustomerId { get; set; }
        public int Userid { get; set; }
        public string UserIp { get; set; }
    }

    public class ChangeOfflineAlertsStatusRequest
    {
        public int NID { get; set; }
        public int NSTATUS { get; set; }
        public int NUPDATEDBY { get; set; }
        public string NUPDATEDUSER { get; set; }
        public int NRETURN { get; set; }
        public int CurrentStatus { get; set; }
        public string UserIp { get; set; }
    }

    public class GetOfflineAlertsDetailsRequest
    {
        public int Alertid { get; set; }
        public int Status { get; set; }
        public int CreatedByUser { get; set; }
        public int Return { get; set; }
        public string RequestPage { get; set; }
    }
#endregion
}
