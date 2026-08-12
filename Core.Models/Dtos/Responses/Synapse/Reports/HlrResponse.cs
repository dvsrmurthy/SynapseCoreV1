using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class HlrMain
    {
        public List<HlrResponse> HlrResponseView { get; set; }
    }
  public  class HlrResponse
    {
      public string Totallookupsprocessed { get; set; }
      public string Numberofvalidnumbers { get; set; }
      public string Percentageofvalidnumbers { get; set; }
      public string Numberofinvalidnumbers { get; set; }
      public string Percentageofinvalidnumbers { get; set; }

    }

}
