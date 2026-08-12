using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models.Dtos.Requests.Synapse.StatusMonitor;
using Synapse.Web.Helpers.SecureAccess;

namespace Synapse.Web.Models
{
    public class StatusServerTransactionRptMain
    {
        public StatusServerTransactionRpt statusServerTransactionRpt { get; set; }
        public List<StatusServerTransactionRpt> statusServerTransactionRpts { get; set; }
    }
    public class StatusServerTransactionRpt
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; } 
        public string Customer { get; set; }
        public string UserId { get; set; }        
        public string Sender { get; set; }
        public string MobileNo { get; set; }        
        public string UserIp { get; set; }
        public string UserName { get; set; }
        public string ReceivedDate { get; set; }
        public string Credits { get; set; }
        public string DataCode { get; set; }
        public string CharCount { get; set; }
        public string ModuleName { get; set; }
        public string ClientPrId { get; set; }
        public string Message { get; set; }
        public int IncomingUserId { get; set; }
        public List<StatusServerTransactionRpt> buildmodel(string UserId, 
            string UserIp, string startDate, string endDate, string sender, string mobileno)
        {
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                var response = clientAcces.GetServerTransactionAsync(new StatusServerTransactionRpt
                {
                    UserId = UserId, StartDate = startDate, EndDate = endDate, Sender = sender, MobileNo = mobileno
                });

                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new StatusServerTransactionRpt
                    {
                        UserName = x.UserName,
                        Sender = x.Sender,  
                        MobileNo = x.MobileNo,
                        Message = x.Message, 
                        ReceivedDate = x.ReceivedDate.Replace(" 00:00:00", "").Trim(), 
                        Credits = x.Credits, 
                        DataCode = x.DataCode,
                        CharCount = x.CharCount, 
                        ModuleName = x.ModuleName,
                        ClientPrId = x.ClientPrId,
                        IncomingUserId = x.IncomingUserId,  
                    }).ToList() : new List<StatusServerTransactionRpt>();
            }
        }
    }
}