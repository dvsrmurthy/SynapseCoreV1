using System.Collections.Generic;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    public class SetVendorsRequest
    {
        /// <summary>
        /// db param name @Nid
        /// </summary>
        public int VendorId { get; set; }

        public string VendorName { get; set; }

        public int CurrentStatus { get; set; }

        public List<PackageDetails> PackageDetailses { get; set; }

        //db param name @nFlag
        public int Flag { get; set; }

        public int CreatedBy { get; set; }

        public int Nretval { get; set; }

        public string command { get; set; }
        public string TechnicalEmail { get; set; }
        public string TechnicalPhone { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessPhone { get; set; }
        public string UserIp { get; set; }
    }

    public class PackageDetails
    {
        public string Package { get; set; }

        public int NoOfAccounts { get; set; }

        public int NoOfSessions { get; set; }
    }
}
