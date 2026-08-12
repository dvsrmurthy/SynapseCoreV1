using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
   public class CountryMasterResponse
    {
       public int CountryCode { get; set; }
       public string? Name { get; set; }
       
       public int STATUS { get; set; }
       public int CurrentStatus { get; set; }

       public string? RejectedReason { get; set; }



    }
}
