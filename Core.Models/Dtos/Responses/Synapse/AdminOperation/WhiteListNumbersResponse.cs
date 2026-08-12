using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AdminOperation
{
    public class WhiteListNumbersResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int SenderId { get; set; }        
        public string MobileNo { get; set; }        
        public bool Status { get; set; }
        public string CreatedOn { get; set; }        
        public int CreatedBy { get; set; }      
        public string CustomerName { get; set; }
        public string UserName { get; set; }
    }
}
