using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Reports
{
    public class ExternalCampaignRequest
    {
        public int UserId { get; set; }
        public int Radio { get; set; }
        public string? StrFromDate { get; set; }
        public string? StrToDate { get; set; }
        public string? SenderId { get; set; }
        public string? UserIp { get; set; }
        public int isDownload { get; set; }
        public int ReturnValue { get; set; }
        public int UID { get; set; }
    }
}
