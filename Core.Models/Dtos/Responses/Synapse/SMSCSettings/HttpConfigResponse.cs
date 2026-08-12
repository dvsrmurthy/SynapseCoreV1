using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    class HttpConfigResponse
    {
        public string Id { get; set; }
        public string PushUrl { get; set; }
        public string DlrUrl { get; set; }
    }
}
