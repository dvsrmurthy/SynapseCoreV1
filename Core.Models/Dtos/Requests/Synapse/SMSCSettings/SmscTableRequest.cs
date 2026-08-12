namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    public class SmscTableRequest
    {
        public int CustomerId { get; set; }

        public int UserId { get; set; }

        public int SenderId { get; set; }

        public string Dispatch { get; set; }

        public int CreatedBy { get; set; }

        public int Status { get; set; }

        public int MapSenderId { get; set; }

        public string ReuestedPage { get; set; }

        public string SearchText { get; set; }

        public string UserIp { get; set; }
    }

    public class SMSCRoutes
    {
        public int userId { get; set; }
        public int customerId { get; set; }
        public string countries { get; set; }
        public string SenderIds { get; set; }
        public string routes { get; set; }
        public string dispatchSenderIds { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
        public string ReuestedPage { get; set; }
        public string MapSenderId { get; set; }
        public string UserIp { get; set; }
        public bool DNDByPass { get; set; }
        public bool DNSByPass { get; set; }
        public int routeAlreadyExist { get; set; } = 0;
    }
}
