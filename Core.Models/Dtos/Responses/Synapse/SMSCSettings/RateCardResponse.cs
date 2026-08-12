using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    public  class RateCardResponse
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string? VendorName { get; set; }
        public string? Package { get; set; }
        public int PackageId { get; set; }
        public string? Country { get; set; }
        public int CountryCode { get; set; }
        public string? Rate { get; set; }
        public string? Currency { get; set; }
        public int STATUS { get; set; }
        public int CurrentStatus { get; set; }
        public string? RateEdit { get; set; }
        public string? CountryName { get; set; }
        public string? RejectReason { get; set; }
        public int countryCode { get; set; }
        public int OperatorId { get; set; }
        public int smscId { get; set; }
        public string? senderType { get; set; }
        public string? SMSCName { get; set; }
        public string? CreatedOn { get; set; }
        public string? OperatorName { get; set; }
        public string? Remarks { get; set; }
        public string? UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }  
        public string? AdditionalCol1 { get; set; }
    }

    public class InsertRateCardResponse 
    {
        public int VendorId { get; set; }
        public string? Package { get; set; }
        public string? Country { get; set; }
        public decimal Rate { get; set; }
        public string? Currency { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int CountryCode { get; set; }
        public int PackageId { get; set; }        
    }

    public class CheckerRateCardResponse {

        public string? CurrentStatus { get; set;}
        public string? RejectReason { get; set; }
        public int UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }

    }

    public class PackagesbyVIdRes {

        public string? packageDetails { get; set; }
    
    }


}
