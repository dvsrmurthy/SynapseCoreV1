using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class NewSmsQueryMain
    {
        public List<SmsQueryResponce> SmsQueryResponce { get; set; }
        public List<SmsQueryReportView> SmsQueryReportView { get; set; }
        //public DataTable SmsQueryReportView { get; set; }
    }
    public class SmsQueryResponce
    {
        public string UserName { get; set; }
       public string MsgID { get; set; }
       public string Customer { get; set; }
       public string Originator { get; set; }
       public string MSISDN { get; set; }
       public string Credits { get; set; }
       public string Accepted { get; set; }
       public string Updated { get; set; }
       public string Delivered { get; set; }
       public string Operator { get; set; }
       public string Country { get; set; }
       public string Vendor { get; set; }
       public string Submitted { get; set; }
       public string updatedDate { get; set; }
        public string DeliveredDate { get; set; }
       public string Status { get; set; }
       public string OperatorPrId { get; set; }
       public string Message { get; set; }
        public string Category { get; set; }
    }
   public class SmsQueryReportView
   {
       public string Count { get; set; }
       public string DlrPercentage { get; set; }
       public string Msgstatus { get; set; }
   }
}
