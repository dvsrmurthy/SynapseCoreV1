using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{

    public class MISReportResponseMain
    {
        public List<MISReportResponseCwise> MISReportResponseCwise { get; set; }
        public List<MISReportResponseUwise> MISReportResponseUwise { get; set; }
        public List<MISReportResponseSwise> MISReportResponseSwise { get; set; }
        public List<MISReportResponseRwise> MISReportResponseRwise { get; set; }
        public string? MISTotal { get; set; }
        public string? Category { get; set; }
    }
    public class MISReportResponseCwise
    {
        public string? CustomerName { get; set; }
        public string? TotalCredits { get; set; }
        public string? MISTotal { get; set; }
    }
    public class MISReportResponseUwise
    {
        public string? UserName { get; set; }
        public string? TotalCredits { get; set; }
        public string? MISTotal { get; set; }
    }
    public class MISReportResponseSwise
    {
        public string? SenderId { get; set; }
        public string? TotalCredits { get; set; }
        public string? MISTotal { get; set; }
    }
    public class MISReportResponseRwise
    {
        public string? CountryName { get; set; }
        public string? OperatorName { get; set; }
        public string? TotalCredits { get; set; }
        public string? MISTotal { get; set; }
    }
}
