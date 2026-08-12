using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserMoKeyWordConfig
{
    public class WinnerRequest
    {
        public List<WinnerReportRequest> WinnerReportRequest { get; set; }
        public string? Id { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public string? requestedby { get; set; }
        public string? Customer { get; set; }
        public string? Campaign { get; set; }
        public string? Winner { get; set; }
        public string? Consolation { get; set; }
        public int Currentstatus { get; set; }
        public string? command { get;set; }
        public int CustomerId { get; set; }
        public int CampId { get; set; }
        public int UserId { get; set; }
        public string? Winnersettingname { get; set; }
        public string? Winnerstartdate { get; set; }
        public string? Winnerenddate { get; set; }
        public string? UserName { get; set; }
        public string? UserIp { get; set; }
    }
    public class WinnerReportRequest {
        public int CampaignId { get; set; }
        public int WinnerType { get; set; }
        public int WinnersettingsnameId { get; set; }
        public string? UserIp { get; set; }
    }
}
