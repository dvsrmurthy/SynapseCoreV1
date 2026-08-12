using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models.Dtos.Requests.Synapse.StatusMonitor;
using Core.Models.Dtos.Responses.Synapse.StatusMonitor;
using Synapse.Web.Helpers.SecureAccess;

namespace Synapse.Web.Models
{
    public class StatusSMSCMain
    {
        public SMSCSearch SMSCSearch { get; set; }
        public List<StatusSMSC> StatusSMSCs { get; set; }
    }
    public class StatusSMSC
    {
        public int SMSCID { get; set; } = 0;
        public string SMSCName { get; set; } = string.Empty;
        public string VendorName { get; set; } = string.Empty;
        public string SMSCStatus { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string SystemId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ThroughPut { get; set; } = string.Empty;
        public string DTon { get; set; } = string.Empty;
        public string DNpi { get; set; } = string.Empty;
        public string STon { get; set; } = string.Empty;
        public string SNpi { get; set; } = string.Empty;
        public string Sessions { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;
        public string DCS { get; set; } = string.Empty;
        public string Transmitter { get; set; } = string.Empty;
        public string Transciever { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;
        public string SystemType { get; set;} = string.Empty;
        public List<StatusSMSC> buildmodel(int userId, string UserIp, bool status, string searchStr)
        {
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                var response = clientAcces.GetSMSCMasterAsync(new SMSCSearch
                {
                    userId = userId, UserIp = UserIp, searchStr = searchStr, status = status
                });

                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new StatusSMSC
                    {
                        SMSCID = x.SMSCID,
                        SMSCName = x.SMSCName,
                        VendorName = x.VendorName,
                        SMSCStatus = x.SMSCStatus,
                        Host = x.Host,
                        Port = x.Port, SystemId = x.SystemId,
                        Password = x.Password,
                        ThroughPut = x.ThroughPut,
                        DTon = x.DTon,
                        DNpi = x.DNpi,
                        STon = x.STon,
                        SNpi = x.SNpi, Sessions = x.Sessions,
                        Instance = x.Instance,
                        DCS = x.DCS,
                        Transmitter = x.Transmitter,
                        Transciever = x.Transciever,
                        Receiver = x.Receiver,
                        SystemType = x.SystemType
                    }).ToList() : new List<StatusSMSC>();
            }
        }
    }
}