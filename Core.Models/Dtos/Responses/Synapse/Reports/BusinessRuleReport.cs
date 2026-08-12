using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class NewBusinessRuleReport
    {
        public List<BusinessRuleReportResponse> BusinessRuleReportResponse { get; set; }
        public List<BusinessRuleReportSecondset> BusinessRuleReportSecondset { get; set; }
    }

    public class BusinessRuleReportResponse
    {
        
        public string? UserId { get; set; }
        public string? ModuleName { get; set; }
        public string? TotalCount { get; set; }
        public string? SubmittedCount { get; set; }
        public string? PendingCount { get; set; }
        public string? DeliveredCount { get; set; }
        public string? FailedCount { get; set; }
    }

    public class BusinessRuleReportSecondset
    {
        public string? BusinessRule { get; set; }
        public string? AlertName { get; set; }
        public string? Query { get; set; }
        public string? Submitted { get; set; }
        public string? Delivered { get; set; }
        public string? Undelivered { get; set; }
    }

    
}
