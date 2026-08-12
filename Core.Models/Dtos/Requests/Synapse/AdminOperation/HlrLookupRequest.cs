using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AdminOperation
{
    public class HlrLookupRequest
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int SenderId { get; set; }
        public bool LookUpEnableForCampaigns { get; set; }
        public bool LookUpEnableForAlerts { get; set; }
        public int HoldPeriodForAbsent { get; set; }
        public int LookUpReAttempt { get; set; }
        public bool SendSmsInRoaming { get; set; }
        public bool IsActive { get; set; }
        public int CurrentStatus { get; set; }
        public string? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Cstatus Fstatus { get; set; }
        public string? RejectNote { get; set; }
        public string? command { get; set; }
        public int UpdatedBy { get; set; }
        public int Returnvalue { get; set; } 
    }

    public class HlrLookupRequestMain
    {
        public List<HlrLookupRequest> HlrLookupRequest { get; set; }
    }
}
