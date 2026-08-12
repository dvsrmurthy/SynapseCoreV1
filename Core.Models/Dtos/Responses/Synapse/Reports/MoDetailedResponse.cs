using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class MoDetailedResponseMain {
        public List<MoDetailedResponse> MoDetailedResponse { get; set; }
    }
    public class MoDetailedResponse
    {
        public string? Name { get; set; }
        public string? Keyword { get; set; }
        public string? MobileNo { get; set; }
        public string? Message { get; set; }
        public string? ReceivedDate { get; set; }
        public string? Credits { get; set; }
        public string? Charges { get; set; }
        public string? ShortCode { get; set; }
        public string? Status { get; set; }
    }
}
