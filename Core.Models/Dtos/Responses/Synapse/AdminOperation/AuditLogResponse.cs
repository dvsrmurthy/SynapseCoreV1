using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AdminOperation
{
    public class AuditLogsResponse
    {
        public int RowID { get; set; }
        public string? UserName { get; set; }
        public string? CustomerName { get; set; }
        public string? PageName { get; set; }
        public string? DBActionName { get; set; }
        public string? CreatedOn { get; set; }
        public string? Oldvalue { get; set; }
        public string? NewValue { get; set; }
        public string? SubmitedOn { get; set; }
        public string? Result { get;set;}
    }

    public class AuditLogsDetailedResponse
    {
        public string? Parameter { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }

    }
}
