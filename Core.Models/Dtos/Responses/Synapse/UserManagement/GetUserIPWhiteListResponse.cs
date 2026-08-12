using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserManagement
{
    public class GetUserIPWhiteListResponse
    {
        public int UserId { get; set; }
        public int CustomerID { get; set; }
        public string Value { get; set; }
        public int CurrentStatus { get; set; }
        public string RejectReason { get; set; }
        public int AutoID { get; set; }
        public int Createdby { get; set; }
        public bool Status { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public int AppType { get; set; }
    }

    public class SetUserIPWhitelistResponse 
    {
        public int CustomerID { get; set; }
        public int UserId { get; set; }
        public string APIIpAddress { get; set; }
        public int Status { get; set; }
        public int CurrentStatus { get; set; }
        public int Createdby { get; set; }
        public string CreatedOn { get; set; }
    }
}
