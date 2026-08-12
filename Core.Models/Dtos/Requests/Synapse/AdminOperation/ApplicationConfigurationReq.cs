using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AdminOperation
{
    public class ApplicationConfigurationReq
    {
        public int Interval { get; set; }
        public bool Suspend { get; set; }
        public int UpdatedBy { get; set; }

    }
}
