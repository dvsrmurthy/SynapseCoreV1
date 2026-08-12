using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.HMS.HealthAlerts.AdminOnRequest
{
   public class GetAlertTypesRequest
   {
       public int UserId { get; set; }
   }
   public class AlertTypesOnRequest
    {
       public string TRANSACTIONTYPE { get; set; }
       public string TRANSNAME { get; set; }
       public string TRANSMSGTYPE { get; set; }
       public int Id { get; set; }
    }
   public class RemoveSegmentsOnrequest
   {
       public string TransType { get; set; }
   }
    //public class ImportSegments
    //{
    //    public string SegmentName { get; set; }
    //    public int NodeType { get; set; }
    //    public string ParentNode { get; set; }
    //    public string Description { get; set; }
    //}
    public class ImportSegmentsListOnRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<SegmentOnRequest> SegmentsList { get; set; }
    }
     public class SegmentOnRequest
    {
        public string SegmentName { get; set; }
        public string NodeType { get; set; }
        public string ParentNode { get; set; }
        public string Description { get; set; }
          
    }
    public class GetSegmentsOnRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
