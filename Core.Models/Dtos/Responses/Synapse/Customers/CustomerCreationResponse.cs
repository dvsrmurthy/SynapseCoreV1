using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Customers
{
    public class CustomerCreationResponse
    {
        public bool ConditionalResponse { get; set; }

        public int ResultResponse { get; set; }

        public string? Merssage { get; set; }

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? Pin { get; set; }

        public string? Mobile { get; set; }

        public string? Fax { get; set; }

        public string? Email { get; set; }

        public string? ContactPerson { get; set; }

        public string? Logo { get; set; }

        public string? FooterNotes { get; set; }

        public int CustomerType { get; set; }

        public int CreateToCustomerCount { get; set; }

        public string? ExpiryDate { get; set; }

        public int ParentId { get; set; }

        public int Status { get; set; }

        public string? Remarks { get; set; }

        public int CreatedBy { get; set; }

        public string? Customer { get; set; }

        public int CurrentStatus { get; set; }

        public string? RejectNote { get; set; }

        public string? TechnicalEmail { get; set; }
        public string? TechnicalPhone { get; set; }
        public string? BusinessEmail { get; set; }
        public string? BusinessPhone { get; set; }
        public int AcmId { get; set; }   //Added by Murty
    }
}

