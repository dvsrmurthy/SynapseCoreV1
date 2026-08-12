using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class NewCampaignAnalysisMain
    {
        public List<CampaignAnalysisResponse> CampaignAnalysisResponse { get; set; }
    }
    public class CampaignAnalysisResponse
    {
        public string UserName { get; set; }
        public string MsgID { get; set; }
        public string Customer { get; set; }
        public string Originator { get; set; }
        public string mobileno { get; set; }
        public string CampaignName { get; set; }
        public string sentdate { get; set; }
        public string donedate { get; set; }
        public string msgstatus { get; set; }
        public string credits { get; set; }
        public string message { get; set; }
        public string camprefid { get; set; }
        public string IncomingUserId { get; set; }
        public string OutboundSender { get; set; }
    }
}
