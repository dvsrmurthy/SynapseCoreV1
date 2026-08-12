using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    public class PreferedRouteRequest
    {
        public int NID { get; set; }
        public int NROUTEID { get; set; }
        public int NCOUNTRYCODE { get; set; }
        public int NCUSTID { get; set; }
        public int STRUSERID { get; set; }
        public int NSERIESID { get; set; }
        public bool NSTATUS { get; set; }
        public int NCREATEDBY { get; set; }
        public string requestedby { get; set; }
        public string UserIp { get; set; }
        public string SearchText { get; set; }
    }

    public class AddPreferedRouteReq
    {
        public int NROUTEID { get; set; }
        public int NCUSTOMERID { get; set; }
        public string NUSERID { get; set; }
        //public int STRCOUNTRYCODE { get; set; }
        public string STRCOUNTRYCODE { get; set; }
        public int NSERIES { get; set; }
        public int NCREATEDBY { get; set; }
        public int Id { get; set; }
        public int EventType { get; set; }
        public int UPDATEDBY { get; set; }
        public int CurrentStatus { get; set; }
        public string command { get; set; }
        public int NRETVAL { get; set; }
        public string UserIp { get; set; }
        public string strNROUTEID { get; set; }
        public string strNSERIES { get; set; }
    }

    public class CheckerPreferedRoute
    {
        public int Id { get; set; }
        public int Currentstatus { get; set; }
        public string Rejectnote { get; set; }
        public int Updatedby { get; set; }
        public int Returnvalue { get; set; }
       
    }

    public class PreferedStatus
    {
        public string CustomerId { get; set; }
        public int Status { get; set; }
        public int createdby { get; set; }
        public int Return { get; set; }
        public string chgstatus { get; set; }
        public string UserIp { get; set; }
    }
}
