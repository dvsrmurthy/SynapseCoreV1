using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models.Dtos.Requests.Synapse.StatusMonitor;
using Synapse.Web.Helpers.SecureAccess;

namespace Synapse.Web.Models
{
    public class PromoSummaryMain
    {
        public PromoSummary PromoSummary { get; set; }
        public List<PromoSummary> PromoSummarys { get; set; }
    }
    public class PromoSummary
    {
        public string Date { get; set; }
        public string Customer { get; set; }
        public int IncomingUserId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Sender { get; set; }
        public int ReceivedCount { get; set; }
        public int SentCount { get; set; }
        public string ModuleName { get; set; }
        public List<PromoSummary> buildmodel(int loggedUserId, string UserIp, string DateCreated, string customer, string user, string sender)
        {
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                var response = clientAcces.GetPromoSummaryAsync(new PromoSummarySearch
                {
                    user = user, UserIp = UserIp, SearchDate = DateCreated, loggedUserId = loggedUserId, customer = customer, sender = sender   
                });

                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new PromoSummary
                    {
                        Customer = x.Customer,
                        UserName = x.UserName,
                        Sender = x.Sender,
                        ReceivedCount = x.ReceivedCount,
                        ModuleName = x.ModuleName,
                        Date = x.Date.Replace(" 00:00:00", "").Trim(),
                        IncomingUserId = x.IncomingUserId,
                        SentCount = x.SentCount
                    }).ToList() : new List<PromoSummary>();
            }
        }
    }
}