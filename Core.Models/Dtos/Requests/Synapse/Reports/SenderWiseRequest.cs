using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Reports
{
    public class SenderWiseRequest
    {
        public string? FromDate { get; set; }
        public string? Todate { get; set; }
        public string? Customer { get; set; }
        public string? SubCustomer { get; set; }
        public string? User { get; set; }
        public string? Sender { get; set; }
        public string? Route { get; set; }
        public int Return { get; set; }
        public string? UserIp { get; set; }
        public int UID { get; set; }
        public int isDownload { get; set; }
    }
}
