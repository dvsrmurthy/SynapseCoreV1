using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.HMS.HealthAlerts.UserOnResponse
{
   public class ConfigAlertsOnResponse
    {
       public int Id { get; set; }
       public string? FirstName { get; set; }
       public string? MiddleName { get; set; }
       public string? LastName { get; set; }
       public string? UserName { get; set; }
       public string? UserFullName { get; set; }
       public string? Password { get; set; }
    }

    public class SegmentsConfigOnResponse
    {
        public int AUTOID { get; set; }
        public string? TRANSACTIONTYPE { get; set; }
        public string? SEGMENT { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? SEGMENTTYPE { get; set; }
        public string? SEGMENTPARENT { get; set; }
        public string? STATUS { get; set; }
    }

    public class SegmentsSelectedOnResponse
    {
        public int AUTOID { get; set; }
        public string? TRANSACTIONTYPE { get; set; }
        public string? SEGMENT { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? SegmentType{get;set;}
        public string? SEGMENTPARENT { get; set; }
        public int STATUS { get; set; }
    }

    public class SenderIdsOnResponse
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Status { get; set; }
        public string? SmscId { get; set; }
        public string? Sender { get; set; }
        public string? OutboundSMSC { get; set; }
    }

    public class TransTypesCustomerOnResponse
    {
        public int AUTOID { get; set; }
        public string? NAME { get; set; }
        public string? TRANSACTIONTYPE { get; set; }
        public string? MSGTYPE { get; set; }
        public int STATUS { get; set; }
        public int ADDEDBY { get; set; }
        public int CUSTID { get; set; }
    }

    public class SegmentsforTemplateOnResponse
    {
        public int AUTOID { get; set; }
        public string? PLACEHOLDER { get; set; }
        public int SEGID { get; set; }
    }

    public class SegmentForTransTypeOnResponse
    {
        public int AUTOID { get; set; }
        public int TRANSID{get;set;}
        public string? FILETYPE { get; set; }
        public string? SOURCETYPE { get; set; }
        public string? FILEPATH { get; set; }
        public string? SENTPATH { get; set; }
        public string? FAILEDPATH { get; set; }
        public string? DLRPATH { get; set; }
        public string? SOURCEIP { get; set; }
        public string? SOURCEPORT { get; set; }
        public string? DESTIP { get; set; }
        public string? DESTPORT { get; set; }
        public string? MSGTEMPLATE { get; set; }
        public int SEGMENTID { get; set; }
        public int CUSTOMERID { get; set; }
        public int USERID { get;set;}
        public int SENDERID { get; set; }

    }

    public class FilePathsAlertTypeOnResponse
    {
        public int AUTOID { get; set; }
        public int TRANSID { get; set; }
        public string? FILETYPE { get; set; }
        public string? SOURCETYPE { get; set; }
        public string? FILEPATH { get; set; }
        public string? SENTPATH { get; set; }
        public string? FAILEDPATH { get; set; }
        public string? DLRPATH { get; set; }
        public string? SOURCEIP { get; set; }
        public string? SOURCEPORT { get; set; }
        public string? DESTIP { get; set; }
        public string? DESTPORT { get; set; }
        public string? MSGTEMPLATE { get; set; }
        public int SEGMENTID { get; set; }
        public int CUSTOMERID { get; set; }
        public int USERID { get; set; }
        public int SENDERID { get; set; }
    }

    public class GetSegmentsbyAutoIdOnResponse
    {
        public int AUTOID { get; set; }
        public string? TRANSACTIONTYPE { get; set; }
        public string? SEGMENT { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? SEGMENTPARENT{get;set;}
    }

    public class GetAlertTemplatedetOnResponse
    {
        public int AUTOID { get; set; }
        public int TRANSID { get; set; }
        public string? FILETYPE { get; set; }
        public string? SOURCETYPE { get; set; }
        public string? FILEPATH { get; set; }
        public string? SENTPATH { get; set; }
        public string? FAILEDPATH { get; set; }
        public string? DLRPATH { get; set; }
        public string? MSGTEMPLATE { get; set; }
        public int SEGMENTID { get; set; }
        public string? SEGMENT { get; set; }
        public string? SEGMENTPARENT { get; set; }

    }
       
}
