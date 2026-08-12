using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class DownloadDlrResponseMain
    {
        public List<DownloadDlrResponse> DownloadDlrResponse { get; set; }
        public List<DownloadResponsecount> DownloadResponsecount { get; set; }
    }

    public class DownloadDlrResponse
    {
        public string Date { get; set; }
        public string UserName { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public string MobileNo { get; set; }
        public string DlvrdStatus { get; set; }
        public string ReceivedDate { get; set; }
        public string SentDate { get; set; }
        public string DlrReceived { get; set; }
    }

    public class DownloadResponsecount
    {
        public int Totalcount { get; set; }
    }
}
