using System.Collections.Generic;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class SmsTrafficMain
    {
        public List<SmsTrafficDaysOfMonthResponse> SmsTrafficDaysOfMonthResponse { get; set; }

        public List<AdminReportView> AdminReportView { get; set; }

        public ReportStrip ReportStrip { get; set; }
    }

    public class ReportStrip
    {
        public string? TotalSMS { get; set; }

        public string? Delivered { get; set; }

        public string? Submitted { get; set; }

        public string? Failed { get; set; }
    }

    public class SmsTrafficDaysOfMonthResponse
    {
        public string? Letter { get; set; }

        public int Freq { get; set; }
    }

    public class AdminReportView
    {
        public string? Customer { get; set; }

        public string? SmsMt { get; set; }

        public string? SmsCost { get; set; }
    }
}
