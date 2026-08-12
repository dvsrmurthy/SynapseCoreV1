using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class ExternalCampaignResponseMain
    {
        public List<ExternalCampaignResponse> ExternalCampaignResponse { get; set; }
    }
    public class ExternalCampaignResponse
    {
        public string CampaignName { get; set; }
        public string TotalCount { get; set; }
        public string CreditsPerMessage { get; set; }
        public string TotalcreditsUsed { get; set; }
        public string TotalSubmitted { get; set; }
        public string TotalDelivered { get; set; }
        public string TotalUndelivered { get; set; }
        public string Schedule { get; set; }
    }
}
