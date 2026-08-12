namespace Core.Models.Dtos.Requests.Synapse.Reports
{
   public class SmsTrackingRequest
    {
       public string FromDate { get; set; }
       public string ToDate { get; set; }
       public int userId { get; set; }
       public string Senderid { get; set; }
       public int CustomerId { get; set; }
       public string mobileno { get; set; }
       public string messageid { get; set; }
       public string country { get; set; }
       public string Operator { get; set; }
       public string status { get; set; }
       public int Return { get; set; }
       public int EventType { get; set; }
    }
}
