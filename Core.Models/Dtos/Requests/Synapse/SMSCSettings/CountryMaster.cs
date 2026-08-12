using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    public class CountryDetails
    {
        public string STRSEARCH { get; set; }
        public int NSEARCH { get; set; }
        public int nCreatedBy { get; set; }
        public string RequestPage { get; set; }
        public string UserIp { get; set; }
    }

    //code added 20-09-2016   	

    public class AddEditCountry
    {
        public string StrCountry { get; set; }
        //public string StrCode { get; set; }
        public int AddUpdateUser { get; set; }
        public string CountryCode { get; set; }
        public int CodeOld { get; set; }
        public string UserIp { get; set; }
        public int UserID { get; set; }
        public int CurrentStatus { get; set; }
        public string command { get; set; }
       
    }

    public class ApproveOrRejectRequestCountry
    {
        public int ISDCode { get; set; }

        public int CurrentStatus { get; set; }

        public string RejectReason { get; set; }

        public int UpdatedBy { get; set; }
    }
}
