using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserManagement
{
    public class GetUsersRequest
    {
        public int NUSERID { get; set; }
        public int NSTATUS { get; set; }
        public int NCUSTID { get; set; }
        public int NCREATEDBY { get; set; }
        public string? RequestPage { get; set; }
        public string? UserIp { get; set; }
        public int PName { get; set; }
    }

    public class UpdateUserRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Mail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? MobileNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int divisionid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Http { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Web { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Smtp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Smpp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SessionsCount { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int Createdby { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? command { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Ldap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Hashkey { get; set; }

        /// <summary>
        /// EVENT TYPES :- 1 - ADD || 2 - EDIT || 3 - APPROVE || 4 - REJECT
        /// </summary>
        public int EventType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UpdatedBy { get; set; }

        public string? RejectNote { get; set; }
        public bool IsDefaultCustomer { get; set; }
        public int CreateToCustomerCount { get; set; }
        public string? UserIp { get; set; }
        public bool IsTwoFactor { get; set; }
        public bool IsPromotional { get; set; }
        public bool IsService { get; set; }
        public bool IsAwareness { get; set; }
        public bool IsAlert { get; set; }
        public bool IsPersonalMessage { get; set; }
    }
}

