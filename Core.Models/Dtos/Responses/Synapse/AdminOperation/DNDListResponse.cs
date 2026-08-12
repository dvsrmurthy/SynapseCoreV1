using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AdminOperation
{
    public class DNDListResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int SenderId { get; set; }
        public int Shortcode { get; set; }
        public string? MobileNo { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }
        public string? CreatedOn { get; set; }
        public string? AuditDate{get;set;}
        public string? Code { get; set; }
        public string? CreatedBy { get; set; }
        public int CurrentStatus { get; set; }
        public int Fstatus { get; set; }
        public int custId { get; set; }
        public int Custname { get; set; }
        //public string? CustomerName { get; set; }
        public string? RejectNote { get; set; }

    }
    public class ExportDNDRes
    {
        public string? MobileNumber { get; set; }
        public string? Name { get; set; }
    }
}
