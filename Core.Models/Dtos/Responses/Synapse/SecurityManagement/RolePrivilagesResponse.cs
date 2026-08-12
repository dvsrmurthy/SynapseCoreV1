using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SecurityManagement
{
    public class FeatureMainResponse
    {
        public List<FeatureResponse> AllFeatures { get; set; }

        public List<FeatureResponse> FeaturesByRoleId { get; set; } 
    }

    public class FeatureResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<SubFeatureResponse> SubFeatures { get; set; }
    }

    public class SubFeatureResponse
    {
        public int SubFeatureId { get; set; }

        public string SubFeatureName { get; set; }

        public string PageType { get; set; }

        public bool IsChecked { get; set; }
    }
}
