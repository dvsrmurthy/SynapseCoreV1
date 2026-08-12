using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class VendorFinanceviewMain
    {
        public List<VendorFinanceManageReportView> VendorFinanceManageReportView { get; set; }
        public List<VendorFinanceCountryOperatorTotalSms> VendorFinanceCountryOperatorTotalSms { get; set; }
    }

    public class VendorFinanceManageReportView
    {
        public string? Vendor { get; set; }

        public string? SMSMT { get; set; }
    }

    public class VendorFinanceCountryOperatorTotalSms
    {
        public string? Country { get; set; }

        public string? Operator { get; set; }
        public string? Vendor { get; set; }

        public string? TotalSMSPushed { get; set; }
    }
}
