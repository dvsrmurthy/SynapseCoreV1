using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
   public class FinanceMain
    {
       public List<FinanceManageReportView> FinanceManageReportView { get; set; }
       public List<FinanceCountryOperatorTotalSms> FinanceCountryOperatorTotalSms { get; set; }
    }


    public class FinanceManageReportView
    {
        public string? Day { get; set; }

        public string? SMSMT { get; set; }
    }

    public class FinanceCountryOperatorTotalSms
    {
        public string? Country { get; set; }

        public string? Operator { get; set; }
        public string? Vendor { get; set; }

        public string? TotalSMSPushed { get; set; }
    }
}
