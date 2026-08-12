using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AdminOperation
{
    public class UnlockUserRequest
    {
        public int USERID { get; set; }
        public int UPDATEDBY { get; set; }
        public string UserIp { get; set; }
    }
    public class UpdateLockStatus
    {
        //Checker Properties
        public int UserId { get; set; }
        public int UpdatedBy { get; set; }
        public int EventType { get; set; }
        public string RejectNote { get; set; }
        public int ReturnValue { get; set; }
        public string UserIp { get; set; }
        public string command { get; set; }
        public bool status { get; set; }
        public int Functionalstatus { get; set; }
    }
}
  
