using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.ManageMobilityCenter
{
    public class CampaignTimingsOnResponse
    {
        public int Id { get; set; }
        public int CampaignTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public int CurrentStatus { get; set; }
        public string RejectNote { get; set; }
    }
    public class CampainTimingsLoadCampOnResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public bool Status { get; set; }
    }
}
