using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
  public  class VendorMain
    { 
      public List<VendorSmsTrafficDaysOfMonthResponse> VendorSmsTrafficDaysOfMonthResponse { get; set; }

      public List<VendorRep> VendorReportView { get; set; }

      public List<VendorCountryOperatorTotalSms> VendorCountryOperatorTotalSmss { get; set; }

      public vendorstrip vendorstrip { get; set; }
       
    }

  public class vendorstrip
  {
      public string? TotalSMS { get; set; }
      public string? Delivered { get; set; }
      public string? Submitted { get; set; }
      public string? Failed { get; set; }
      public string? PeakThroughput { get; set; }
  }

  public class VendorSmsTrafficDaysOfMonthResponse
  {
      public string? Letter { get; set; }

      public int Freq { get; set; }
  }
  public class VendorCountryOperatorTotalSms
  {
      public string? Country { get; set; }

      public string? Operator { get; set; }

      public string? TOTALSMS { get; set; }
  }

    public class VendorRep
    {
        public string? Day { get; set; }

        public int MT { get; set; }

        public int MO { get; set; }
    }

    public class VendorReportView
    {
        public string? Date { get; set; }

        public int MT { get; set; }

        public int MO { get; set; }

    }
  

   

}
