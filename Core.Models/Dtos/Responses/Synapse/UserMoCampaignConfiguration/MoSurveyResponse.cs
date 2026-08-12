using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserMoCampaignConfiguration
{
  public class MoSurveyResponse
    {
        public int ID { get; set; }
        public string Name {get; set;}
        public string Code {get; set;}
        public string CampaignName { get; set; }
        public string SurveyDate {get; set;}
        public string SurveyEndDate { get; set; }
        public string Status {get; set;}
    }

  public class MoSenderGetResponse
  {
      public int Id { get; set; }

      public string Code { get; set; }
  }

  public class MOMobileLengthValidationResponse
  {
      public int SenderId { get; set; }

      public int MobileLength { get; set; }

      public int CountryCode { get; set; }

      public int TotalLength { get; set; }

      public string Name { get; set; }

      public string series { get; set; }
  }
  public class MOCampaignbyuserresponse
  {
      public int Id { get; set; }
      public string CampaignName { get; set; }
  }

  public class MoSureveySaveResponse {
      public string Name { get; set; }
    public int SenderId {get; set;}
    public int Language {get; set;}
      public string FilePath {get; set;}
      public string FileName {get; set;}
      public string Message {get; set;}
      public DateTime SurveyDate {get; set;}
      public string SheetName {get; set;}
      public string MobileField {get; set;}
      public int TotalCredits {get; set;}
      public int Status {get; set;}
      public int CurrentStatus { get; set; }
      public int InValidCount { get; set; }
      public int ProcessedCount { get; set; }
      public int TotalCount { get; set; }
      public int CreatedBy {get; set;}
      public int CreatedOn { get; set; }

  }

}
