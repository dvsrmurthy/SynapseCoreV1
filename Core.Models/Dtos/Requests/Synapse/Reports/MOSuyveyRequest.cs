using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Reports
{
    public class MOSuyveyRequestmain
    {
        public List<MOSuyveyRequest> MOSuyveyRequest { get; set; }
    }
    public class MOSuyveyRequest
    {
        public string StrFromDate { get; set; }
        public string StrToDate { get; set; }
        public string UserId { get; set; }
        public string Shortcode { get; set; }
        public string Keyword { get; set; }
        public int ReturnValue { get; set; }
        public int SurveyId { get; set; }
        public string SearchString { get; set; }
    }
}
