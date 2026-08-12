using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.Analytics
{
    public class DashBoardAnalyticsRequest
    {
        public int Days { get; set; }

        public int Bind { get; set; }

        public int Smid { get; set; }

        public int Day { get; set; }

        public string? Text { get; set; }

        public int UserId { get; set; }
    }
    public class SMSMOAnalyticsRequest
    {
        public int Days { get; set; }

        public int Bind { get; set; }

        public int Smid { get; set; }

        public int Day { get; set; }

        public string? Text { get; set; }

        public int UserId { get; set; }
        public string? Shortcode { get; set; }
    }


}
