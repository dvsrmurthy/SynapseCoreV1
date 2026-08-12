using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserMoKeyWordConfig
{
    public class ShortcodeAnalyticResponse
    {
        public List<ShortcodeAnalyticsGraph> ShortcodeAnalyticsGraph { get; set; }
    }
    public class ShortcodeAnalyticsGraph
    {
        public string Hour { get; set; }

        public string Day { get; set; }

        public string Count { get; set; }

        public string Letter { get; set; }
        public int Freq { get; set; }
    }
}
