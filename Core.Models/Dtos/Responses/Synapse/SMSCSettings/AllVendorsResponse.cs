using System.Collections.Generic;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    public class AllVendorsResponse
    {
        public int Id { get; set; }

        public string VendorName { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; }

        public string PackageDetails { get; set; }

        public string NoOfAccounts { get; set; }

        public string NoOfSessions { get; set; }

        public string Name { get; set; }

        public int CurrentStatus { get; set; }

        public int Nretval { get; set; }
        public string Rejectnote { get; set; }
        public string TechnicalEmail { get; set; }
        public string TechnicalPhone { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessPhone { get; set; }

        public List<PackageDetailsResponse> PackageDetailsCollection { get; set; }
    }
    public class AllVendorsResponseRate
    {
        public int Id { get; set; }
        public int SMSCId { get; set; }

        public string VendorName { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; }

        public string PackageDetails { get; set; }

        public string NoOfAccounts { get; set; }

        public string NoOfSessions { get; set; }

        public string Name { get; set; }

        public int CurrentStatus { get; set; }

        public int Nretval { get; set; }
        public string Rejectnote { get; set; }
        public string TechnicalEmail { get; set; }
        public string TechnicalPhone { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessPhone { get; set; }

        public List<PackageDetailsResponse> PackageDetailsCollection { get; set; }
    }

    public class PackageDetailsResponse
    {
        public string Package { get; set; }

        public int NoOfAccounts { get; set; }

        public int NoOfSessions { get; set; }
    }
}
