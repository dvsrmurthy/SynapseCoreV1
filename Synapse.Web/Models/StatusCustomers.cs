using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models.Dtos.Requests.Synapse.StatusMonitor;
using Core.Models.Dtos.Responses.Synapse.StatusMonitor;
using Synapse.Web.Helpers.SecureAccess;

namespace Synapse.Web.Models
{
    public class StatusCustomerMain
    {
        public CustomerSearch CustomerSearch { get; set; }
        public List<StatusCustomer> StatusCustomers { get; set; }
    }
    public class StatusCustomer
    {
        public int CustomerId { get; set; } = 0;
        public string Customer { get; set; } = string.Empty;
        public string AccountManager { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CreditType { get; set; } = string.Empty;
        public string AvailableCredits { get; set; } = string.Empty;
        public string CreatedOn { get; set; } = string.Empty;
        public string ParentCustomer { get; set; } = string.Empty;        
        public List<StatusCustomer> buildmodel(int userId, string UserIp, bool status, string searchStr)
        {
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                var response = clientAcces.GetCustomerMasterAsync(new CustomerSearch
                {
                    userId = userId, UserIp = UserIp, searchStr = searchStr, status = status
                });

                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new StatusCustomer
                    {
                        CustomerId = x.CustomerId,
                        Customer = x.Customer,
                        AccountManager = x.AccountManager,
                        Status = x.Status,
                        CreatedOn = x.CreatedOn,
                        CreditType = x.CreditType,
                        AvailableCredits = x.AvailableCredits,  
                        ParentCustomer = x.ParentCustomer
                    }).ToList() : new List<StatusCustomer>();
            }
        }
    }
}