using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Reports
{

    public class MoDetailedRequestMain {
        public List<MoDetailedRequest> MoDetailedRequest { get;set; }
    }
    public class MoDetailedRequest
    {
        public string? StrFromDate { get; set; }
        public string? StrToDate { get; set; }
        public string? UserId { get; set; }
        public string? Shortcode { get; set; }
        public string? Keyword { get; set; }
        public int ReturnValue { get; set; }
        public string? SearchBy { get; set; }
        public string? UserIp { get; set; }
        public int UIP { get; set; }
        public int isDownload { get; set; }
        public int IsEncrypt { get; set; }
    }
}
