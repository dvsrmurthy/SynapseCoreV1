namespace Core.Models.Dtos.Requests.Synapse.Reports
{
    public class CampaignDetailedRequest
    {
       public string? CampId { get; set; }
       public string? Mobile { get; set; }
       public int Return { get; set; }
       public int UID { get; set; }
       public string? UserIp { get; set; }
       public int isDownload { get; set; }
    }
}
