using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.EmailAndPushNotifications
{
    public class EmailAnalysisResponseMain
    {
        public List<EmailAnalysisResponse> EmailAnalysisResponse { get; set; }
    }

    public class EmailAnalysisResponse
    {
        public string Date { get; set; }
        public string CampaignId { get; set; }
        public string Campaign { get; set; }
        public string TotalEmailCount { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Status { get; set; }
        public string EmailCount { get; set; }
    }
}
