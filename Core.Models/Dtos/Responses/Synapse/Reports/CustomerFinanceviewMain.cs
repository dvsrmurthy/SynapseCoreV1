using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class CustomerFinanceviewMain
    {
        public List<CustomerFinanceManageReportView> CustomerFinanceManageReportView { get; set; }
        public List<CustomerFinanceCountryOperatorTotalSms> CustomerFinanceCountryOperatorTotalSms { get; set; }
        public List<CustomerMTReportViews> CustomerMTReportView { get; set; }
    }
    public class CustomerFinanceManageReportView
    {
        public string Customer { get; set; }

        public string SMSMT { get; set; }
    }

    public class CustomerFinanceCountryOperatorTotalSms
    {
        public string Country { get; set; }

        public string Operator { get; set; }
       

        public string TotalSMSPushed { get; set; }
    }
    public class CustomerMTReportViews
    {
        public string Day { get; set; }

        public string SMSMT { get; set; }
    }
}
