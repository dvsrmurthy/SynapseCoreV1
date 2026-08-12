using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserMoKeyWordConfig
{
    public class ShortcodeAnalyticRequest
    {
        public int Days { get; set; }

        public int Day { get; set; }

        public string? Shortcode { get; set; }

        public int UserId { get; set; }

        public string? FromDate { get; set; }

        public string? ToDate { get; set; }
        
    }
}
