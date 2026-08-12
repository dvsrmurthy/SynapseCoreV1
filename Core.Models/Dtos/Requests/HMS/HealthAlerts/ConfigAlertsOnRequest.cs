using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.HMS.HealthAlerts.UserOnRequest
{
    public class ConfigAlertsOnRequest
    {
        public int CustId { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
    }
    public class SegmentsConfigOnRequest
    {
        public string TransId { get; set; } 
    }
    public class SegmentsSelectedOnRequest
    {
        public string SelectedSegments { get; set; } 
    }
    public class SenderIdsOnRequest
    {
        public int UserId { get; set; }
        public string RequestedBy { get; set; }
        public int Retval { get; set; }
    }
    public class TransTypeCustomerOnRequest
    {
        public int CustomerId { get; set; }
        public string TransType { get; set; }
    }
    public class SegmentsforTemplateOnRequest
    {
        public int TemplateId { get; set; }
    }
    public class DeletePlaceHoldersOnRequest
    {
        public int TransTypeId { get; set; }
    }
    public class SetPlaceholdersOnRequest
    {
        public int TemplateId { get; set; }
        public int SegId { get; set; }
        public int RetVal { get; set; }
        public string StrErr { get; set; }
    }
    public class SegmentForTransTypeOnRequest
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int TransType { get; set; }
    }
    public class FilePathsAlertTypeOnRequest
    {
        public string FilePath { get; set; }
        public int AlertType { get; set; }
        public int Status { get; set; }
        public int Retval { get; set; }
        public string ReturnMsg { get; set; }
    }
    public class AlertTemplatesOnRequest
    {
        public int TransTypeId { get; set; }
        public string FileType { get; set; }
        public int SourceType { get; set; }
        public string FilePath { get; set; }
        public string SentFilePath { get; set; }
        public string FailedFilePath { get; set; }
        public string DlrPath { get; set; }
        public string SourceIp { get; set; }
        public string SourcePort { get; set; }
        public string DestIp { get; set; }
        public string DestPort { get; set; }
        public string MsgTemplate { get; set; }
        public string PlcaeHolders { get; set; }
        public int SegMobileNoId { get; set; }
        public int Status { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int SenderId { get; set; }
        public int CreatedBy { get; set; }
        public int Retval { get; set; }
        public string StrErr { get; set; }
    }
    public class GetSegmentsbyAutoIdOnRequest
    {
        public string StrAutoIds { get; set; }
    }
    public class GetAlertTemplatedetOnRequest
    {
        public int TemplateId { get; set; }
        public int Status { get; set; }
        public string StrRetMsg { get; set; }
    }
   
}
