using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SecurityManagement
{
    public class RolesCreationRequest
    {

        //public int nroleid { get; set; }
        //public int nStatus { get; set;}
        //public int nReseller { get; set;}
        //public int nCreatedBy { get; set;}
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string? UserIp { get; set; }

    }


    public class EditRolesCreation
    {
        public int RoleId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; }

        public int CurrentStatus { get; set; }

        public int CustomerId { get; set; }

        public bool IsReseller { get; set; }

        public int CreatedBy { get; set; }

        public string? command { get; set; }
        
        public int EventType { get; set; }

        public string? RejectNote { get; set; }

        public int UpdatedBy { get; set; }

        public string? UserIp { get; set; }
    }
}
