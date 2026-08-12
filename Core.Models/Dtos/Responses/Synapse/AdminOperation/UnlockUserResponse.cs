using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AdminOperation
{
    public class UnlockUserResponse
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? UserName { get; set; }
        public string? LockTime { get; set; }
        public bool Status { get; set; }
        public int Currentstatus { get; set; }
        public int Functionalstatus { get; set; }
        public string? RejectNote { get; set; }
    }
}
