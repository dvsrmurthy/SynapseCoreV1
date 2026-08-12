using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Customers
{
    public class MapDivisionsResponse
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? CUSTOMER { get; set; }
        public int DivisonID { get; set; }
        public string? DivisionName { get; set; }
        public int MobilityId { get; set; }
        public string? CenterName { get; set; }
       // public string? CreditType { get; set; }
        public int Credits { get; set; }
        public string? CustomerName { get; set; }
        public bool STATUS { get; set; }
    }
}
