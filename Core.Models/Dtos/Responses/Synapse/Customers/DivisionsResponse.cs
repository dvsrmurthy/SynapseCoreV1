using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Customers
{
    public class DivisionsResponse
    {
        public int Id {get; set;}
        public string?  Name {get; set;}
        public string? Description {get; set;}
        public bool Status { get; set; }
        public string? CustomerName { get; set; }
    }
}
