using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Reports
{
    public class DownloadDlrRequest
    {
        public string FromDate { get; set; }
        public string Todate { get; set; }
        public string Customer { get; set; }
        public string UserName { get; set; }
        public string Sender { get; set; }
        public string UserIp { get; set; }
        public int isDownload { get; set; }
        public int UID { get; set; }
        public string MobileNo { get; set; }
    }
}
