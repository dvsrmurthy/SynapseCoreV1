using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.ManageMobilityCenter
{
    public class ManageMobilityCenterOnRequest
    {
        public int MobilityId { get; set; }
        public string? Status { get; set; }
    }
}
