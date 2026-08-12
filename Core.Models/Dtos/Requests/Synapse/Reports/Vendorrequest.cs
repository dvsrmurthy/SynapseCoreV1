using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Reports
{
    public class Vendorrequest
    { 
        public string? FromDate { get; set; }

        public string? ToDate { get; set; }

        public string? VendorId { get; set; }

        public string? userid { get; set; }

        public string? SenderId { get; set; }

        public string? Country { get; set; }

        public string? Operator { get; set; }
    }
}
