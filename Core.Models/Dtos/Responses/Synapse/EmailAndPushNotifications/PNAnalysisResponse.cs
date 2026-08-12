using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.EmailAndPushNotifications
{
    public class PNAnalysisResponseMain
    {
        public List<PNAnalysisResponse> PNAnalysisResponse { get; set; }
    }
    public class PNAnalysisResponse
    {
        public string Date { get; set; }
        public string CampaignId { get; set; }
        public string Campaign { get; set; }
        public string PNCampaign { get; set; }
        public string TotalPNCount { get; set; }
        public string KeyLabel { get; set; }
        public string DeviceId { get; set; }
        public string Status { get; set; }

    }
}
