using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Reports
{
   public class CampaignAnalysisRequest
    {
        public string CampId { get; set; }
        public string Mobile { get; set; }
        public int Return { get; set; }
        public int UID { get; set; }
        public string UserIp { get; set; }
    }
}
