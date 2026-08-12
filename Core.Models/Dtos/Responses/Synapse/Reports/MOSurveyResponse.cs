using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.Reports
{
    public class MOSurveyResponsemain
    {
        public List<MOSurveyResponse> MOSurveyResponse { get; set; }
    }
    public class MOSurveyResponse
    {
        public string Shortcode { get; set; }
        public string Name { get; set; }
        public string ReceivedDate { get; set; }
        public string Credits { get; set; }
        public string TotalCredits { get; set; }
        public string MobileNumber { get; set; }
        public string Response { get; set; }

        public string SurveyQuestion { get; set; }
        public string SurveyName { get; set; }
    }
}
