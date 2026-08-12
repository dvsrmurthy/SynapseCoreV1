using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserManagement
{
    public class SynapseUser
    {
        public int Userid { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string MobileNo { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int divisionid { get; set; }
        public string DivisionName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public int Level { get; set; }
        public bool Http { get; set; }
        public bool Smtp { get; set; }
        public bool Web { get; set; }
        public bool IsTwoFactor { get; set; }
        public bool Smpp { get; set; }
        public int SessionsCount { get; set; }
        public bool Status { get; set; }
        public string NAME { get; set; }
        public int CurrentStatus { get; set; }
        public string RejectNote { get; set; }
     
        //public string DecryptPassword { get; set; }
        public bool Ldap { get; set; }
        public string Hashkey { get; set; }
        public int Fstatus { get; set; }

        public string CreatedByName { get; set; }
        public bool IsPromotional { get; set; }
        public bool IsService { get; set; }
        public bool IsAwareness { get; set; }
        public bool IsAlert { get; set; }
        public bool IsPersonalMessage { get; set; }
    }
}
