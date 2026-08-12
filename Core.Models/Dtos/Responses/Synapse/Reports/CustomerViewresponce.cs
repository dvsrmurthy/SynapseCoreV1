using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class CustomerSmsTrafficMain
    {
        public List<CustomerSmsTrafficDaysOfMonthResponse> CustomerSmsTrafficDaysOfMonthResponse { get; set; }

        public List<CustomerRep> CustomerReportView { get; set; }

        public List<CustomerViewresponce> CustomerViewresponce { get; set; }

        public List<CountryOperatorTotalSms> CountryOperatorTotalSmss { get; set; }
        public Customeranotherreport Customeranotherreport { get; set; }
       
    }

    public class Customeranotherreport
    {
        public string? TotalSMS { get; set; }
        public string? Delivered { get; set; }
        public string? Submitted { get; set; }
        public string? Failed { get; set; }
    }
   

    public class CustomerSmsTrafficDaysOfMonthResponse
    {
        public string? Letter { get; set; }

        public int Freq { get; set; }
    }

    public class CountryOperatorTotalSms
    {
        public string? Country { get; set; }

        public string? Operator { get; set; }

        public string? TOTALSMS { get; set; }
    }

    public class CustomerRep
    {
        public string? Day { get; set; }

        public int SMSMT { get; set; }

        public int SMSMO { get; set; }
    }

    public class CustomerReportView
    {
        public string? Date { get; set; }

        public int MT { get; set; }

        public int MO { get; set; }

    }

    public class CustomerViewresponce
    {
        public int MsgID { get; set; }
        public string? Customer { get; set; }
        public string? Originator { get; set; }
        public string? MSISDN { get; set; }
        public string? Accepted { get; set; }
        public string? Updated { get; set; }
        public string? Delivered { get; set; }
        public string? Operator { get; set; }
        public string? Vendor { get; set; }
        public string? Submitted { get; set; }
        public string? updatedDate { get; set; }
        public string? DeliveredDate { get; set; }
        public string? Status { get; set; }
    }
  
   
}
