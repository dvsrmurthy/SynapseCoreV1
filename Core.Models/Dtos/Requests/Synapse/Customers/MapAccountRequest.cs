using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Customers
{
    public class MapAccountRequest
    {
        public int Actmgrid { get; set; }
        public string customerId { get; set; }
        public string UserIp { get; set; }
        public string beforeSelection { get; set; }
        public string afterSelection { get; set; }
        public string addedCustomers { get; set; }
        public int LoginUserId { get; set; }
    }
}
