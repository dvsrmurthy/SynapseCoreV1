using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    public class RateCardRequest
    {
        public int NRATEID { get; set; }
        public int NSTATUS { get; set; }
        public int NVENDORID { get; set; }
        public int nCreatedby { get; set; }
        public int NRETVAL{get; set;}
        //public int nVenderId { get; set; }
        public string UserIp { get; set; }
        public string SearchText { get; set; }
        public string SearchCountryHistory { get; set; }
        public string SearchVendorHistory { get; set; }
        public int rc { get; set; }
    }

    public class InsertRateCardRequest
    {
        public int nId { get; set; }
        public int nCountryCode { get; set; }
        public int nOperatorId { get; set; }
        public int nVendorId { get; set; }
        public int nSmscId { get; set; }
        public string senderType { get; set; }
        public string rate { get; set; }
        public string UserIp { get; set; }
        public string Remarks { get; set; }
        public string command { get; set; }
        public int CurrentStatus { get; set; }
        public int nUserId { get; set; }
        public int returnValue { get; set; }
    }

    public class CheckerRateCardRequest
    {
        public int ID { get; set; }
        public int CURRENTSTATUS {get; set;}
        public int RETURNVALUE {get; set;}
        public int UPDATEDBY {get; set;}
        public string REJECTNOTE {get; set;}

    }

    public class PackagesbyVIdReq {

        public int VendorId { get; set; }
    
    }


}
