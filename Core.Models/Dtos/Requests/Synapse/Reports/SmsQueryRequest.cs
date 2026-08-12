namespace Core.Models.Dtos.Requests.Synapse.Reports
{
    public class SmsQueryRequest
    {
        public string? Customer { get; set; }
        public string? User { get; set; }
        public string? Role { get; set; }
        public string? Period { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? userId { get; set; }
        public string? Senderid { get; set; }
        public string? CustomerId { get; set; }
        public string? mobileno { get; set; }
        public string? messageid { get; set; }
        public string? country { get; set; }
        public string? Operator { get; set; }
        public string? status { get; set; }
        public int Return { get; set; }
        public int EventType { get; set; }
        public int UID { get; set; }
        public string? UserIp { get; set; }
        public int isDownload { get; set; }
        public string? SearchText { get; set; }
    }
}
