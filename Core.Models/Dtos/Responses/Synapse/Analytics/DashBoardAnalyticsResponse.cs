using System.Collections.Generic;
using Core.Models.Dtos.Requests.Synapse.UserCampaigns;

namespace Core.Models.Dtos.Responses.Synapse.Analytics
{
    public class DashBoardAnalyticsResponse
    {
        public List<AnalyticsGraph> AnalyticsGraph { get; set; }
        

        public Modules Modules { get; set; }

        public List<PullSms> PullSmses { get; set; }

        public List<Smsc> Smsc { get; set; }

        public SuccessRatio SucessRatio { get; set; }

        public List<Tps> Tp { get; set; }

        public List<CampaignActivity> CampaignActivities { get; set; }
        public List<WorldMap> WorldMaps { get; set; }

        // TO DO with pull sms class
    }
    public class SMSMOResponse{
        public List<SMSMOAnalyticsResponse> SMSMOAnalyticsResponse { get; set; }
        public List<SMSMOMinuteResponse> SMSMOMinuteResponse { get; set; }
    }

    public class AnalyticsGraph
    {
        public string Hour { get; set; }

        public string Day { get; set; }

        public string Count { get; set; }
    }
    public class SMSMOAnalyticsResponse
    {
        //public string Hour { get; set; }
        public string Day { get; set; }
        public string Count { get; set; }
    }
    public class SMSMOMinuteResponse
    {
        public int LastMinute { get; set; }
        public int MOCount { get; set; }
    }

    public class ModulesCollection
    {
        public string Count { get; set; }

        public string Module { get; set; }
    }

    public class Modules
    {
        public int Camp { get; set; }

        public int Qsms { get; set; }

        public int Alerts { get; set; }
    }
    
    public class PullSms
    {
        public string Hour { get; set; }

        public string Day { get; set; }

        public string Count { get; set; }

        public decimal ProcessTime { get; set; }

      //  public int Days { get; set; }

      //  public int UserId { get; set; }
    }

    public class Smsc
    {
        public int Id { get; set; }

        public string SmscName { get; set; }

        public string Status { get; set; }

        public string Progress { get; set; }

        public string Tps { get; set; }
        
        public string Action { get; set; }
    }

    public class SucessRatioCollection
    {
        public string Count { get; set; }

        public string DlrStatus { get; set; }
    }

    public class SuccessRatio
    {
        public int Delivrd { get; set; }

        public int UnDeliv { get; set; }

        public int Submitted { get; set; }
    }

    public class Tps
    {
        public string Hour { get; set; }

        public string Day { get; set; }

        public string Tp { get; set; }
    }

    public class CampaignActivity
    {
        public string CampName { get; set; }

        public string UserName { get; set; }

        public string ScheduleDate { get; set; }

        public string CampaignType { get; set; }
        
        public string Progress { get; set; }

        public string Percentage { get; set; }
    }
    
    public class WorldMap
    {
        public int Id { get; set; }
        public string key { get; set; }
        public int doc_count { get; set; }
        public string delivery_rate { get; set; }
    }

    public class WorldMapMock
    {
        public int took { get; set; }

        public bool timed_out { get; set; }

        public _shards _shards { get; set; }

        public hitss hits { get; set; }

        public aggregations aggregations { get; set; }
    }

    public class _shards
    {
        public int total { get; set; }

        public int successful { get; set; }

        public int failed { get; set; }
    }

    public class hitss
    {
        public int total { get; set; }

        public int max_score { get; set; }

        public hitss[] hits { get; set; }
    }

    public class aggregations
    {
        public world_map world_map { get; set; }
    }

    public class world_map
    {
        public int doc_count_error_upper_bound { get; set; }

        public int sum_other_doc_count { get; set; }

        public buckets[] buckets { get; set; }
    }

    public class buckets
    {
        public string key { get; set; }

        public int doc_count { get; set; }

        public string delivery_rate { get; set; }
    }
}
