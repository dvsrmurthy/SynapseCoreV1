using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.DBAlerts
{
    # region OnlineAlerts
    public class DBAlertsResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int SenderId { get; set; }
        public string? Code { get; set; }
        public int RuleId { get; set; }
        public string? Rulename { get; set; }
        public string? SQL { get; set; }

        public int StatusUpdated { get; set; }
        public string? SRCTable { get; set; }
        public string? UniqueIdColumn { get; set; }
        public string? StatusColumn { get; set; }
        public string? CustomerMessage { get; set; }
        public string? MobileField { get; set; }
        public string? PlaceHolders { get; set; }
        public int Language { get; set; }
        public string? Message { get; set; }
        public int DlrRequired { get; set; }
        public string? Schedule { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
        public int DBtype { get; set; }
        public string? ConnectionString { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectNote {get;set;   }
    }

    public class SetAlertsRes
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int SenderId { get; set; }
        public string? Code { get; set; }
        public int RuleId { get; set; }
        public string? Rulename { get; set; }
        public string? MobileField { get; set; }
        public string? MessageField { get; set; }
        public string? PlaceHolders { get; set; }
        public int Language { get; set; }
        public string? MessageTemplate { get; set; }
        public string? CustomerMessage { get; set; }
        public int DlrRequired { get; set; }
        public string? Schedule { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public int Status { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectNote { get; set; }
        public string? CustomerName { get; set; }
        public int SendNowOrScheduleLater { get; set; }
        public string? CreatedByName { get; set; }
        public int IntervalType { get; set; }
        public string? IntervalValue { get; set; }
        public int StartTimeHour { get; set; }
        public int StartTimeMinute { get; set; }
        public int StartTimeSecond { get; set; }
        public string? SendAltEverDay { get; set; }
        public string? SendAltWekDay { get; set; }
        public string? SendAltWekDayTime { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string? ServerIP { get; set; }
        public int BankId { get; set; }
        public string? BankName { get; set; }
        public string? EMAILField { get; set; }
        public int EMAILTemplateId { get; set; }
        public string? EMAILTEMPNAME { get; set; }
        public string? EMAILTemplateText { get; set; }
    }

    public class GetBusinessRulesResponse
    {
        public int RuleId { get; set; }
        public string? RuleName { get; set; }
        public string? DBQuery { get; set; }
        public string? DBType { get; set; }
        public string? ConnectionString { get; set; }
    }

    public class GetSenderResponse
    {
        public int Id { get; set; }
        public string? Code { get; set; }
    }

    public class GetTemplatesResponse
    {
        public int TempID { get; set; }
        public string? TemplateName { get; set; }
        public string? Text { get; set; }
        public string? TextEditor { get; set; }
        public int Type { get; set; }
    }

    public class GetOnlineAlertsDetailsResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ServerIP { get; set; }
        public int SenderId { get; set; }
        public string? SenderIDFiled { get; set; }
        public string? Code { get; set; }
        public int RuleId { get; set; }
        public string? Rulename { get; set; }
        public string? MobileField { get; set; }
        public string? MessageField { get; set; }
        public string? PlaceHolders { get; set; }
        public int MessageTemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? MessageTemplate { get; set; }
        public string? CustomerMessage { get; set; }
        public int DlrRequired { get; set; }
        public string? Schedule { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public int Status { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectNote { get; set; }

        public int SendNowOrScheduleLater { get; set; }
        public int IntervalType { get; set; }
        public string? InvervalValue { get; set; }
        public int StartTimeHour { get; set; }
        public int StartTimeMinute { get; set; }
        public int StartTimeSecond { get; set; }
        public string? SendAltEverDay { get; set; }
        public string? SendAltWekDay { get; set; }
        public string? SendAltWekDayTime { get; set; }
        public int CustomerId { get; set; }
        public string? Customer { get; set; }
        public int UserId { get; set; }
        public string? User { get; set; }
        public string? EMAILField{get; set;}
     public int EMAILTemplateId{get; set;}
     public string? EMAILTEMPNAME{get; set;}
     public string? EMAILTemplateText { get; set; }
    }

    public class GetOnlineAlertsDetailsResponseforedit
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ServerIP { get; set; }
        public int SenderId { get; set; }
        public string? SenderIDFiled { get; set; }
        public string? Code { get; set; }
        public int RuleId { get; set; }
        public string? Rulename { get; set; }
        public string? MobileField { get; set; }
        public string? MessageField { get; set; }
        public string? PlaceHolders { get; set; }
        public int TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? MessageTemplate { get; set; }
        public string? CustomerMessage { get; set; }
        public int DlrRequired { get; set; }
        public string? Schedule { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public int Status { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectNote { get; set; }
        

        public int SendNowOrScheduleLater { get; set; }
        public int IntervalType { get; set; }
        public string? InvervalVal { get; set; }
        public int StartTimeHour { get; set; }
        public int StartTimeMinute { get; set; }
        public int StartTimeSecond { get; set; }
        public string? SendAltEverDay { get; set; }
        public string? SendAltWekDay { get; set; }
        public string? SendAltWekDayTime { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? EMAILField { get; set; }
        public int EMAILTemplateId { get; set; }
        public string? EMAILTEMPNAME { get; set; }
        public string? EMAILTemplateText { get; set; }
    }
#endregion

#region OfflineAlerts

    public class GetOfflineAlerts
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string? AlertName { get; set; }
        public string? ServerIP { get; set; }
        public string? MessageField { get; set; }
        public int CampaignType { get; set; }
        public string? Name { get; set; }//campaign name
        public string? customername { get; set; }
        public string? username { get; set; }
        public int DlrRequired { get; set; }
        public int SenderId{ get; set; }
        public int RuleId { get; set; }
        public string? Rulename { get; set; }
        public string? code { get; set; }
        public int CurrentStatus { get; set; }
        public int Status { get; set; }
        public int SendNowOrScheduleLater { get; set; }
        public int IntervalType { get; set; }
        public string? InvervalVal { get; set; }
        public string? MobileField { get; set; }
        public int IsResponseEmailRequired { get; set; }
        public string? EmailTo { get; set; }
        public string? EmailCC { get; set; }
        public string? EmailBCC { get; set; }
        public int StartTimeHour { get; set; }
        public int StartTimeMinute { get; set; }
        public int StartTimeSecond { get; set; }
        public string? SendAltEverDay { get; set; }
        public string? SendAltWekDay { get; set; }
        public string? SendAltWekDayTime { get; set; }
        public string? EMAILField { get; set; }
        public string? EMAILTEMPNAME { get; set; }
        public int EMAILTemplateId { get; set; }
        public string? EMAILTemplateText { get; set; }
        //public bool IsCheckerRequired { get; set; }
        public int BankId { get; set; }
        public string? BankName { get; set; }
        public string? RejectNote { get; set; }
        public string? CreatedByName { get; set; }
    }

    public class GetBusinessOfflineRulesResponse
    {
        public int RuleId { get; set; }
        public string? Rulename { get; set; }
        public string? ShortDesc { get; set; }
        public string? LongDesc { get; set; }
        public string? DBQuery { get; set; }
        public int Status { get; set; }
        public int ProfileId { get; set; }
        public string? Profile { get; set; }
        public string? CustomerName { get; set; }
        public string? DBType { get; set; }
        public string? ConnectionString { get; set; }
    }

    public class DBOfflineAlertsResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int SenderId { get; set; }
        public string? Code { get; set; }
        public int RuleId { get; set; }
        public string? Rulename { get; set; }
        public string? SQL { get; set; }

        public int StatusUpdated { get; set; }
        public string? SRCTable { get; set; }
        public string? UniqueIdColumn { get; set; }
        public string? StatusColumn { get; set; }
        public string? CustomerMessage { get; set; }
        public string? MobileField { get; set; }
        public string? PlaceHolders { get; set; }
        public int Language { get; set; }
        public string? Message { get; set; }
        public int DlrRequired { get; set; }
        public string? Schedule { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
        public int DBtype { get; set; }
        public string? ConnectionString { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectNote { get; set; }
    }

    public class SetOfflineAlertsRes
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int SenderId { get; set; }
        public string? Code { get; set; }
        public int RuleId { get; set; }
        public string? Rulename { get; set; }
        public string? MobileField { get; set; }
        public string? MessageField { get; set; }
        public string? PlaceHolders { get; set; }
        public int Language { get; set; }
        public string? Message { get; set; }
        public string? CustomerMessage { get; set; }
        public int DlrRequired { get; set; }
        public string? Schedule { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public int Status { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectNote { get; set; }

    }

   


    public class GetOfflineAlertsDetailsResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string? AlertName { get; set; }
        public string? Description { get; set; }
        public string? ServerIP { get; set; }
        public string? customername { get; set; }
        public string? username { get; set; }
        public int SenderId { get; set; }
        public string? SenderIDFiled { get; set; }
        public string? Code { get; set; }
        public int RuleId { get; set; }
        public string? Rulename { get; set; }
        public string? MobileField { get; set; }
        public string? MessageField { get; set; }
        public string? PlaceHolders { get; set; }
        public int MessageTemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? MessageTemplate { get; set; }
        public string? CustomerMessage { get; set; }
        public int DlrRequired { get; set; }
        public string? Schedule { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public int Status { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectNote { get; set; }
        public int IsResponseEmailReq { get; set; }
        public string? EmailTo{get;set;}
        public string? EmailCC{get;set;}
        public string? EmailBCC { get; set; }
        public int campaigntype { get; set; }
        public string? campaignname { get; set; }
        //public int CurrentStatus { get; set; }
        public int SendNowOrScheduleLater { get; set; }
        public int IntervalType { get; set; }
        public string? InvervalVal { get; set; }
        public int StartTimeHour { get; set; }
        public int StartTimeMinute { get; set; }
        public int StartTimeSecond { get; set; }
        public string? SendAltEverDay { get; set; }
        public string? SendAltWekDay { get; set; }
        public string? SendAltWekDayTime { get; set; }
        public string? EMAILField { get; set; }
        public int EMAILTemplateId { get; set; }
        public string? EMAILTEMPNAME { get; set; }
        public string? EMAILTemplateText { get; set; }
        public int BankId { get; set; }
        public string? BankName { get; set; }
    }

    public class GetOfflineAlertsDetailsResponseforedit
    {
        public int Id { get; set; }
        public string? AlertName { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string? customername { get; set; }
        public string? username { get; set; }
        public string? Description { get; set; }
        public string? ServerIP { get; set; }
        public int SenderId { get; set; }
        public string? SenderIDFiled { get; set; }
        public string? Code { get; set; }
        public int RuleId { get; set; }
        public string? Rulename { get; set; }
        public int campaigntype { get; set; }
        public string? Name { get; set; }//campaign name
        public string? MobileField { get; set; }
        public string? MessageField { get; set; }
        public string? PlaceHolders { get; set; }
        public int MessageTemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? MessageTemplate { get; set; }
        public string? CustomerMessage { get; set; }
        public int DlrRequired { get; set; }
        public string? Schedule { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public int Status { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        //public int CurrentStatus { get; set; }
        public string? RejectNote { get; set; }
        public int IsResponseEmailReq { get; set; }
        public string? EmailTo { get; set; }
        public string? EmailCC { get; set; }
        public string? EmailBCC { get; set; }
        public int TemplateId { get; set; }
        public int ScheduleIntervalType { get; set; }
        public int SendNowOrScheduleLater { get; set; }
        public string? InvervalVal { get; set; }
        public int IntervalType { get; set; }
        public int StartTimeHour { get; set; }
        public int StartTimeMinute { get; set; }
        public int StartTimeSecond { get; set; }
        public string? SendAltEverDay { get; set; }
        public string? SendAltWekDay { get; set; }
        public string? SendAltWekDayTime { get; set; }
        public int CurrentStatus { get; set; }
        public string? EMAILField { get; set; }
        public int EMAILTemplateId { get; set; }
        public string? EMAILTEMPNAME { get; set; }
        public string? EMAILTemplateText { get; set; }
    }
#endregion
}
