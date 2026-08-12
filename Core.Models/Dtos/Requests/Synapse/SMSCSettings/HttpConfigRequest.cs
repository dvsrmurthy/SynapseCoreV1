using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    class HttpConfigRequest
    {
        public int ID { get; set; }
        public string HttpPushUrl { get; set; }
        public string HttpDlrUrl { get; set; }
	    public int CurrentStatus { get; set; }
	    public int Status { get; set; }
        public int CreatedBy { get; set; }
    }
}
