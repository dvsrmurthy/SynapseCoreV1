using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Customers
{
    public class DivisionsRequest
    {
       public int NDIVID   {get; set;}
       public int nCreatedby  {get; set;}
       public int NSTATUS { get; set; }
       public string? RequestPage { get; set; }
    }
}
