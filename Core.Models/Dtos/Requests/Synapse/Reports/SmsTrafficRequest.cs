namespace Core.Models.Dtos.Requests.Synapse.Reports
{
    public class SmsTrafficRequest
    {
        public string? FromDate { get; set; }

        public string? ToDate { get; set; }

        public string? AccountManagerId { get; set; }

        public string? CustomerId { get; set; }

        public string? SenderId { get; set; }

        public string? Country { get; set; }

        public string? Operator { get; set; }

        public string? Userid { get; set; }

        public int UID { get; set; }

        public string? UserIp { get; set; }
    }
}
