using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserMoCampaignConfiguration
{
   public class MOsurveyRequest
    {
       public int SurveyId { get; set; }
       public int Status { get; set; }
       public int Createdby { get; set; }
       public string requestedby { get; set; }
    }

   public class MoSenderGetRequest
   {
      // public string SearchText { get; set; }
       public int UserId { get; set; }
   }
   public class MOCampaignbyuserrequest
   {
       public int UserId { get; set; }
   }

   public class Mosurveystatus {

       public int CampID { get; set; }
       public int Status { get; set; }
       public int CreatedBy { get; set; }
       public int CurrentStatus { get; set; }
       public int NRETVAL { get; set; }
   }

   public class MoSureveySaveRequest
   {
   public int CustomerID { get; set; }
   public int CampID { get; set; }
   public string Name { get; set; }
   public int SenderId {get; set;}
   public int Language { get; set; }
   public string LanguageName { get; set; }
   public string Message {get; set;}
   public int CharCount {get; set;}
   public int CreditsUsed {get; set;}
   public string XMLSchedule {get; set;}
   public int NumberOfOptions {get; set;}
   public string OptionA {get; set;}
   public string OptionB {get; set;}
   public string OptionC {get; set;}
   public string OptionD {get; set;}
   public int MessageType {get; set;}
   public int Status {get; set;}
   public int CreatedBy {get; set;}
   public string SenderName { get; set; }
   public string ImportFileName { get; set; }
   public string ActualFileName { get; set; }
   public int ValidCount { get; set; }
   public string Schedule { get; set; }
   public int TotalCreditsReq {get; set;}
   public string SheetName {get; set;}
   public string MobileField {get; set;}
   public int CurrentStatus { get; set; }
   public int InvalidCount { get; set; }
   public int ProcessedCount { get; set; }
   public int TotalCount { get; set; }
   public string SurveyEndDate { get; set; }
   public string Footer { get; set; }
   public int MOCampaignId { get; set; }
   public int nReturn { get; set; }
   public int nId { get; set; }
   public int SurveryId { get; set; }
   }
}
