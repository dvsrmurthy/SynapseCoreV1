using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.HlrLookup
{
   public class HlrLookupUploadResponse
    {
       public int Id { get; set; }
       public string FileCreatedTime { get; set; }
       public string Description { get; set; }
       public string FileName { get; set; }
       public int Status { get; set; }
       public string Email { get; set; }
       public int CurrentStatus { get; set; }
       public string ProcessedFilePath { get; set; }
    }
    public class SingleJson
    {
        public string msisdn { get; set; }
        public string result { get; set; }
        public string imsi { get; set; }
        public string location { get; set; }
        public string time { get; set; }
        public string description { get; set; }
    }
    public class CountryMasterHlrResponse
    {
        public int CountryCode { get; set; }
        public string Name { get; set; }

        public int STATUS { get; set; }
        public int CurrentStatus { get; set; }

        public string RejectedReason { get; set; }



    }
}
