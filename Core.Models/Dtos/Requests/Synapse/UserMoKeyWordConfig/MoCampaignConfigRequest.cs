using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserMoKeyWordConfig
{
   public class MoCampaignConfigRequest
    {
        public int CreatedMOId { get; set; }
        public string? Customer { get; set; }
        public string? CustomerID { get; set; }
       
        public string? User { get; set; }
        public string? UserID { get; set; }

        public string? CampaignName { get; set; }
        public int MOCampaignID { get; set; }

        public string? Description { get; set; }
        public string? ShortCodeType { get; set; }
        public int ShortCType { get; set; }

        public string? ShortCode { get; set; }
        public int ShCode { get; set; }
        public string? SenderID { get; set; }

        public string? Keyword { get; set; }
        public int intKwrd { get; set; }

        public string? SubKeyword { get; set; }
        public int intSbKwd { get; set; }

        public string? Outbound { get; set; }
        public int intOutBnd { get; set; }

        public string? SystemID { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

        public int LangSelect { get; set; }
        public string? Language { get; set; }

        public string? ReplyMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public int ForwardType { get; set; }
        public string? HTTPType { get; set; }
        public string? SMPPType { get; set; }
        public string? CustomType { get; set; }

        public string? URL { get; set; }
        public string? MobileNumber { get; set; }
        public string? MobileNumberOrder { get; set; }
        public string? Destination { get; set; }
        public string? DestinationOrder { get; set; }
        public string? Message { get; set; }
        public string? MessageOrder { get; set; }
        public string? DateTime { get; set; }
        public string? DateTimeOrder { get; set; }
        public string? Others { get; set; }
        public string? OthersOrder { get; set; }
        public string? Value { get; set; }
        public string? RetryHour { get; set; }
       
        public string? RetryMinute { get; set; }
        public string? RetrySMPPHour { get; set; }
        public string? RetrySMPPMinute { get; set; }
        public string? SMPPAccount { get; set; }
        public string? Component { get; set; }
        public string? Parameter { get; set; }
        public string? Status { get; set; }

        public int CurrentStatus { get; set; }

        public string? command { get; set; }

        public int AddedUpdaeBy { get; set; }
        public string? requestedby { get; set; }
        public string? UserIp { get; set; }
    }

    public class MoCampaignSerchRequest
    {
        public string? SearchText { get; set; }
        public int CustomerId { get; set; }
    }
}
