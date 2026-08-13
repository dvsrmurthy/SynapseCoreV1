using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synapse.Web.CampaignPlugin.Models
{
    public class SimpleSmsMain
    {
        public SimpleSms SimpleSms { get; set; }
        public List<SimpleSms> SimpleSmsTable { get; set; }
    }
    public class SimpleSms
    {
        public string? CampaignNameorRecipient { get; set; }
        public string? SenderId { get; set; }
        public string? recipients { get; set; }
        public string? Language { get; set; }
        public bool Template { get; set; }
        public int TemplateID { get; set; }
        public bool DeliveryReport { get; set; }
        public string? Message { get; set; }

        public string? CampaignName { get; set; }
        public string? SmsType { get; set; }
        public string? ValidRecipients { get; set; }
        public string? Credits { get; set; }
        public string? StartDate { get; set; }
        public string? Status { get; set; }
        public string? FunctionalStatus { get; set; }
        public string? PreviewMessage { get; set; }
        public int CurrentStatus { get; set; }

        public List<SimpleSms> buildmodel()
        {
            return new List<SimpleSms>
            {
                new SimpleSms 
                {
                    CampaignName="Campaign1",SmsType="Simple SMS",SenderId="Sid1",Language="English",ValidRecipients="10",Credits="1",StartDate="Oct 24, 2014 12:00:00",Status="Submitted",FunctionalStatus="Approved"
                },
                new SimpleSms 
                {
                    CampaignName="Campaign2",SmsType="Simple SMS",SenderId="Sid2",Language="English",ValidRecipients="10",Credits="1",StartDate="Oct 24, 2014 12:00:00",Status="Submitted",FunctionalStatus="Approved"
                },
                new SimpleSms 
                {
                    CampaignName="Campaign3",SmsType="Simple SMS",SenderId="Sid3",Language="English",ValidRecipients="10",Credits="1",StartDate="Oct 24, 2014 12:00:00",Status="Submitted",FunctionalStatus="Approved"
                },

            };
        }
    }
}