using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.HMS.HealthAlerts.AdminOnResponse
{
   public class AlertTypesOnResponse
    {
       public int AUTOID { get; set; }
       public string? TRANSACTIONTYPE { get; set; }
       public string? NAME { get; set; }
       public string? MSGTYPE { get; set; }
       public int STATUS { get; set; }

    }
    public class GetSegmentsOnResponse
    {
        public int AUTOID { get; set; }
        public string? TRANSCATIONTYPE { get; set; }
        public string? SEGMENTNAME { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? NODETYPE { get; set; }
        public string? PARENTNODE { get; set; }
    }
}
