using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    public class SMSCSwithResponse
    {
        public List<SMSCCountryResponse> SMSCCountryResponse { get; set; }
        public List<SMSCUserResponse> SMSCUserResponse { get; set; }
        public List<SMSCSenderResponse> SMSCSenderResponse { get; set; }
        public List<SMSCRouteResponse> SMSCRouteResponse { get; set; }

        public int Id { get; set; }
        public string? SMSCName { get; set; }
        public string? FromSMSC { get; set; }
        public string? ToSMSC { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int Countrycode { get; set; }
        public string? Country { get; set; }
        public int Sender { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
    public class SMSCCountryResponse
    {
        public string? CountryName { get; set; }
        public int CountryCode { get; set; }
    }
    public class SMSCUserResponse
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
    }
    public class SMSCSenderResponse
    {
        public int SenderId { get; set; }
        public string? DisplaySender { get; set; }
    }

    public class SMSCRouteResponse
    {
        public int Id { get; set; }
        public string? RouteName { get; set; }
    }
}
