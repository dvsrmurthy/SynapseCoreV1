using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class NewCampaignSummaryMain
    {
        public List<CampaignSummaryResponce> CampaignSummaryResponce { get; set; }
        public List<CampaignSummarySecondResponce> CampaignSummarySecondResponce { get; set; }
    }
    public class CampaignSummaryResponce
    { 
       public string? UserName { get; set; }
       public string? MsgID { get; set; }
       public string? Customer { get; set; }
       public string? Originator { get; set; }
       public string? mobileno { get; set; }
       public string? Campname { get; set; }
       public string? sentdate { get; set; }
       public string? donedate { get; set; }
       public string? msgstatus { get; set; }
       public string? message { get; set; }

        public int id { get; set; }
        public string? Name {get;set;}
        public string? CreatedOn {get;set;}
        public string? Schedule {get;set;}
        public string? CreatedBy {get;set;}
        public int SenderId{get;set;}
        public string? SenderName {get;set;}
        public int Language{get;set;}
        public int TotalCount{get;set;}
        public int ValidCount{get;set;}
        public int InvalidCount{get;set;}
        public int DeliveredCount { get; set; }
        public int Status { get; set; }
        public int CountryCode {get;set;}
        public string? CREDITS { get; set; }
        public string? Category { get; set; }
    }

    public class CampaignSummarySecondResponce
    {
        public string? Date { get; set; }
        public string? CampaignId { get; set; }
        public string? Campaign { get; set; }
        public string? Sender { get; set; }
        public string? Country { get; set; }
        public string? SMSCount { get; set; }
        public string? Category { get; set; }
    }

}
