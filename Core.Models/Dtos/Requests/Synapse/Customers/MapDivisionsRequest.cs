using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Customers
{
    public class MapDivisionsRequest
    {

        public int NDIVDETID { get; set; }
        public int NDIVID { get; set; }
        public int NSTATUS { get; set; }
        public int NCUSTID { get; set; }
        public int nCreatedby { get; set; }
        public string RequestPage { get; set; }
    }
}
