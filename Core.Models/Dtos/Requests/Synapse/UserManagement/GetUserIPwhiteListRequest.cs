using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserManagement
{
    public class GetUserIPwhiteListRequest
    {
        //public string username { get; set; }
        public int CustomerID { get; set; }
        public int Createdby { get; set; }
        public int NSEARCH { get; set; }
        public int id { get; set; }
        public int AutoID { get; set; }
        public string UserIp { get; set; }
        public int Grid { get; set; }
    }

    public class SetUserIPwhiteListRequest
    {  
         public string Id { get; set; }
         public string CustomerID { get; set; }
         public string USERID { get; set; }
         public string[] IPADDRESS { get; set; }
         public int status { get; set; }
         public int currentstatus { get; set; }
         public int Createdby { get; set; }
         public int ReturnValue { get; set; }
         public string command { get; set; }
         public string UserIp { get; set; }
         public string AppType { get; set; }
    }

}
