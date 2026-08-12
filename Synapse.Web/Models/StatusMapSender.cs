using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models.Dtos.Requests.Synapse.StatusMonitor;
using Synapse.Web.Helpers.SecureAccess;

namespace Synapse.Web.Models
{
    public class StatusMapSenderMain
    {
        public MapSenderSearch MapSenderSearch { get; set; }
        public List<StatusMapSender> StatusMapSenders { get; set; }
    }
    public class StatusMapSender
    {
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string DisplaySender { get; set; }
        public string DespatchType { get; set; }
        public string countryName { get; set; }
        public string DNSBypass { get; set; }
        public string DNDBypass { get; set; }
        public string smscid { get; set; }
        public string SMSCName { get; set; }
        public string status { get; set; }
        public string Operator { get; set; }
        public string Vendor { get; set; }
        public string RouteId { get; set; }
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<StatusMapSender> buildmodel(int UserId, string UserIp, bool status, string searchStr, 
            bool dnsbypass, bool dndbypass)
        {
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                var response = clientAcces.GetMapSenderAsync(new MapSenderSearch
                {
                    userId = UserId, 
                    UserIp = UserIp, 
                    searchStr = searchStr, status = status,
                    DNDBypass = dndbypass,
                    DNSBypass = dnsbypass
                });

                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new StatusMapSender
                    {
                        CustomerName = x.CustomerName, 
                        UserName=x.UserName,
                        DisplaySender=x.DisplaySender,
                        DespatchType=x.DespatchType,
                        countryName = x.countryName,
                        DNSBypass =x.DNSBypass,
                        DNDBypass = x.DNDBypass,
                        smscid = x.smscid,
                        SMSCName = x.SMSCName,
                        status=x.status,
                        Operator = x.Operator,
                        Vendor = x.Vendor,
                        RouteId = x.RouteId,
                        CustomerId = x.CustomerId,
                        UserId = x.UserId,
                        CreatedOn = x.CreatedOn,
                        CreatedBy = x.CreatedBy,
                        UpdatedBy = x.UpdatedBy,    
                        UpdatedOn = x.UpdatedOn,    
                    }).ToList() : new List<StatusMapSender>();
            }
        }
    }
}