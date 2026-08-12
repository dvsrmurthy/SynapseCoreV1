using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class SenderWiseResponseMain
    {
        public List<SenderWiseResponse> SenderWiseResponse { get; set; }
    }
    public class SenderWiseResponse
    {
        public int AUTOID { get; set; }
        public string UserName { get; set; }
        public string Customer { get; set; }
        public string CampaignName { get; set; }
        public string ScheduleDate { get; set; }
        public string SenderID { get; set; }
        public string CountryName { get; set; }
        public int Credits { get; set; }
        public string TotalCredits { get; set; }
    }
}
