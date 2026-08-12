using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.CreditsManagement
{
    public class ApproveCustomerCreditOnRequest
    {
        public int CustomerCreditId { get; set; }
        public int CurrentStatus { get; set; }
        public int UpdatedBy { get; set; }
        //public bool Status { get; set; }
        public string? RejectionReason { get; set; }
    }
}
