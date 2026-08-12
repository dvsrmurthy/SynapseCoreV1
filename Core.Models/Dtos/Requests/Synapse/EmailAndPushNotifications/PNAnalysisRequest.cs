using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.EmailAndPushNotifications
{
    public class PNAnalysisRequest
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CampaignId { get; set; }
        public string SearchText { get; set; }
        public int UID { get; set; }
        public string UserIp { get; set; }
        public int IsDownload { get; set; }
    }
}
