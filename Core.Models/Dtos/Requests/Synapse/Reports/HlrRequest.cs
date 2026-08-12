using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Reports
{
   public class HlrRequest
    {

       public string? strFromDate { get; set; }
       public string? strToDate { get; set; }
       public string? CustomerId { get; set; }
       public string? Senderid { get; set; }
       public string? country { get; set; }
       public string? vendor { get; set; }
       public int Return { get; set; }
    }
}
