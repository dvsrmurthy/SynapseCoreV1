using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
   public  class AccountManagerMain
    {
        public List<AccountManageDaysOfMonthResponse> AccountManageDaysOfMonthResponse { get; set; }

        public List<AccountManageReportView> AccountManageReportView { get; set; }
        public AMAReportstrip AMAReportstrip { get; set; }

    }

      public class AccountManageDaysOfMonthResponse
    {
        public string Letter { get; set; }

        public int Freq { get; set; }
    }

      public class AccountManageReportView
      {
          public string Customer { get; set; }

          public string SMSMT { get; set; }

          
      }

    public class AMAReportstrip
    {
        public string TotalSMS { get; set; }
        public string Delivered { get; set; }
        public string Submitted { get; set; }
        public string Failed { get; set; }
    }
}
