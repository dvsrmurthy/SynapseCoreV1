using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Reports
{
    public class BusinessRuleReportRequest
    {
        public int CustomerId { get; set; }
        public string StrFromDate { get; set; }
        public string StrToDate { get; set; }
        public string UserId { get; set; }        
        public string ModuleName { get; set; }
        public int ReturnValue { get; set; }       
    }
}
