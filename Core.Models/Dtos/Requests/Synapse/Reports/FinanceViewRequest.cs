using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Reports
{
   public class FinanceViewRequest
    {
       public string? FromDate { get; set; }
       public string? ToDate { get; set; }
       public int UID { get; set; }
       public string? UserIp { get; set; }
    }
}
