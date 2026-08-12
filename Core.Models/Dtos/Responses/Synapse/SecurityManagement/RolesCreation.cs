using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SecurityManagement
{
    public class RolesCreation
    {
        public int Id { get; set; }
        public string? NAME { get; set; }
        public string? description { get; set; }
        public bool isreseller { get; set; }
        public bool status { get; set; }
        public int Fstatus { get; set; }
        public string? RejectReason { get; set; }
        public int createdby { get; set; }
        public string? createdon { get; set; }
        //public string? level { get; set; }
        public string? RoleType { get; set; }

        public int CustomerId { get; set; }
       // public int ROLUPDBY { get; set; }
        //public DateTime ROLUPDON { get; set; }
        //public int ROLLVL { get; set; }

    }


}
