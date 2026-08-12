using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class MoSummaryResponseMain
    {
        public List<MoSummaryResponse> MoSummaryResponse { get; set; }
    }
    public class MoSummaryResponse
    {
        public string? Shortcode { get; set; }
        public string? Name { get; set; }
        public string? Keyword { get; set; }
        public string? ReceivedDate { get; set; }
        public string? Credits { get; set; }
        public string? TotalCredits { get; set; }
    }
}
