using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserManagement
{
    public class ApproveIpWhiteListRequest
    {
        public string username { get; set; }

    }
    public class SetIpWhiteListRequest
    {
        public int id { get; set; }
        public int returnvalue { get; set; }
        public string rejectnote { get; set; }
        public int currentstatus { get; set; }
        public int createdby { get; set; }
        
    }
}
