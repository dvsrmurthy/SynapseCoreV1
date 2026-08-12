using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.ManageMobilityCenter
{
   public class CampaignTimingsOnRequest
    {
        public int CAMPID { get; set; }
        public int CAMPTYPEID { get; set; }
        public int STATUS { get; set; }
        public string? UserIp { get; set; }
        public int UserId { get; set; }
    }
   public class CampainTimingsLoadCampOnRequest
   {
       public int CAMPTYPEID { get; set; }
       public string? CAMPTYPENAME { get; set; }
       public int STATUS { get; set; }
   }
   public class CampainTimingsInsertCampOnRequest
   {
       public int CAMPID{get;set;}
       public int CAMPTYPEID { get; set; }
       public string? FROMTIME { get; set; }
       public string? TOTIME { get; set; }
       public int UPDATEDBY{get;set;}
       public int STATUS { get; set; }
       public int CURRENTSTATUS { get; set; }
       public string? UserIp { get; set; }
   }
   public class CampaignTimingsChangeStatusOnRequest
   {
       public string? CAMPID { get; set; }
       public int STATUS { get; set; }
       public int UPDATEDBY { get; set; }
       public int CURRENTSTATUS { get; set; }
       public string? UserIp { get; set; }
   }
   public class CheckerUpdateCampaignTimingsOnRequest
   {
       public int CAMPID { get; set; }
       public int CURRENTSTATUS { get; set; }
       public string? REJECTREASON { get; set; }
       public int UPDATEDBY { get; set; }
       public string? UserIp { get; set; }
   }
}
