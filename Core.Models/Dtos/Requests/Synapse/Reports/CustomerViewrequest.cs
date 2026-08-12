using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Reports
{
   public class CustomerViewrequest
    {

        public string? FromDate { get; set; }

        public string? ToDate { get; set; }

        public string? Userid { get; set; }
      
        public string? CustomerId { get; set; }

        public string? SenderId { get; set; }

        public string? Country { get; set; }

        public string? Operator { get; set; }

        public int UID { get; set; }

        public string? UserIp { get; set; }

    }
}
