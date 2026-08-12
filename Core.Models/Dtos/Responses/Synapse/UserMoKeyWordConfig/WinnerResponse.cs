using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserMoKeyWordConfig
{
    public class WinnerResponse
    {
        public List<WinnerReportResponse> WinnerReportResponses { get; set; }
        public int Id { get; set; }
        public string Customer { get; set; }
        public int UserId { get; set; }
       // public int User { get; set; }
        public string CampaignName { get; set; }
        public bool Status { get; set; }
        public string Winner { get; set; }
        public string Consolation { get; set; }
        public int CurrentStatus { get; set; }
        
        public int CustomerId { get; set; }
        public int CampId { get; set; }
        public string UserName { get; set; }
        public string WinnerSettingName { get; set; }
        public string WinnerStartdate { get; set; }
        public string WinnerEnddate { get; set; }
    }
    public class WinnerReportResponse
    { 
        public string MobileNo { get; set; }
        public string ReceivedDate  { get; set; }
        public string Coupon { get; set; }
    }
}
