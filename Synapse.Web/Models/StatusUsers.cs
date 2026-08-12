using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models.Dtos.Requests.Synapse.StatusMonitor;
using Core.Models.Dtos.Responses.Synapse.StatusMonitor;
using Synapse.Web.Helpers.SecureAccess;

namespace Synapse.Web.Models
{
    public class StatusUsersMain
    {
        public UserSearch UserSearch { get; set; }
        public List<StatusUsers> StatusUsers { get; set; }
    }
    public class StatusUsers
    {
        public int Userid { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool status { get; set; } = false;
        public string Customer { get; set; } = string.Empty;
        public string ParentCustomer { get; set; } = string.Empty;
        public bool http { get; set; } = false;
        public bool web { get; set; } = false;
        public bool smpp { get; set; } = false;
        public bool smtp { get; set; } = false;
        public string CreatedOn { get; set; } = string.Empty;
        public List<StatusUsers> buildmodel(int userId, string UserIp, bool status, string searchStr)
        {
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                var response = clientAcces.GetUsersMasterAsync(new UserSearch
                {
                    UserId = userId, UserIp = UserIp, searchStr = searchStr, status = status
                });

                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new StatusUsers
                    {
                        Userid = x.Userid,
                        UserName = x.UserName,  
                        Password = x.Password,  
                        status = x.status,
                        Customer = x.Customer,  
                        ParentCustomer = x.ParentCustomer,  
                        http = x.http,  
                        web = x.web,    
                        smpp = x.smpp,  
                        smtp = x.smtp,  
                        CreatedOn = x.CreatedOn,  
                    }).ToList() : new List<StatusUsers>();
            }
        }
    }
}