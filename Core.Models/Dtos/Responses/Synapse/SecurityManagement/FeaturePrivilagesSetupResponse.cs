using System.Collections.Generic;

namespace Core.Models.Dtos.Responses.Synapse.SecurityManagement
{
    public class FeaturePrivilagesSetupResponse
    {
        public string? ParentPlugin { get; set; }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string? Plugin { get; set; }

        public string? PageType { get; set; }

        public bool IsCheckerRequier { get; set; }        

        public bool ActiveStatus { get; set; }
    }

    public class FeaturePrivilagesSetupMain
    {
        public List<FeaturePrivilagesSetupResponse> AllFeatures { get; set; }

        public List<FeaturePrivilagesSetupResponse> FeaturesByCustomerId { get; set; }
    }
}
