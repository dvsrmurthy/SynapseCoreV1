using System.Collections.Generic;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    public class MapSenderRequest
    {
        public string? category;

        public int MapSenderId { get; set; }

        public int CustomerId { get; set; }

        public string? UserId { get; set; }

        public int SenderId { get; set; }

        public int ShortCode { get; set; } //code added on 20022017

        public string? DispatchId { get; set; }

        public string? DispatchType { get; set; }

        public int CountryCode { get; set; }

        public int CreatedBy { get; set; }

        public int CurrentStatus { get; set; }

        public string? RejectNote { get; set; }

        public List<CountrySeries> CountrySeries { get; set; }

        public string? Command { get; set; }

        public string? Route { get; set; }

        public int DNSBYPASS { get; set; }

        public int DNDBYPASS { get; set; }
        public int CheckHLR { get; set; }

        public string? UserIp { get; set; }
    }

    public class CountrySeries
    {
        public string? CountryName { get; set; }        

        public int SenderId { get; set; }

        public string? DisplayName { get; set; }

        public string? ShortCode { get; set; }

        public string? Route { get; set; }
        public int DNSBYPASS { get; set; }
        public int DNDBYPASS { get; set; }
        public string? DispatchSenderId { get; set; }
        public string? DispatchType { get; set; }
        public int routeAlreadyExists { get; set; } = 0;
    }
}
