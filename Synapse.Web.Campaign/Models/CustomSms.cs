using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synapse.Web.CampaignPlugin.Models
{
    public class CustomSmsMain
    {
        public CustomSms CustomSms { get; set; }
        public List<CustomSms> CustomSmsTable { get; set; }
    }
    public class CustomSms
    {
        public string? recipients { get; set; }
        public string? SenderId { get; set; }
        public string? Language { get; set; }
        public bool Template { get; set; }
        public int TemplateID { get; set; }
        public bool DeliveryReport { get; set; }
        public bool AllowDuplicates { get; set; }
        public string? ContactsFile { get; set; }
        public string? GroupIds { get; set; }
        public string? MobileNumberField { get; set; }
        public string? MessageField { get; set; }
        public string? SelectTemplate { get; set; }
        public string? Message { get; set; }
        public string? Schedule { get; set; }

        public string? CampaignName { get; set; }
        public string? SmsType { get; set; }
        public string? ValidRecipients { get; set; }
        public string? Credits { get; set; }
        public string? StartDate { get; set; }
        public string? ScheduleTime { get; set; }
        public string? Status { get; set; }
        public string? FunctionalStatus { get; set; }

        public List<CustomSms> buildmodel()
        {
            return new List<CustomSms>
            {
                new CustomSms 
                {
                    CampaignName="Campaign1",SmsType="Simple SMS",SenderId="Sid1",Language="English",ValidRecipients="10",Credits="1",StartDate="Oct 24, 2014 12:00:00",Status="Submitted",FunctionalStatus="Approved"
                },
                new CustomSms 
                {
                    CampaignName="Campaign2",SmsType="Simple SMS",SenderId="Sid2",Language="English",ValidRecipients="10",Credits="1",StartDate="Oct 24, 2014 12:00:00",Status="Submitted",FunctionalStatus="Approved"
                },
                new CustomSms 
                {
                    CampaignName="Campaign3",SmsType="Simple SMS",SenderId="Sid3",Language="English",ValidRecipients="10",Credits="1",StartDate="Oct 24, 2014 12:00:00",Status="Submitted",FunctionalStatus="Approved"
                },

            };
        }
    }
}