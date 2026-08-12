using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
   public class SenderIDRequest
    {
       //get
       //public string STRNAME { get; set; }
       public int NSENDERID { get; set; }
       public int NSTATUS { get; set; }
       public int nCreatedby { get; set; }
       public string RequestPage { get; set; }
       public string UserIp { get; set; }
        public string SearchString { get; set; }
    }

    public class AddEditSender
    {
        //set-insert/update
        public int sId { get; set; }
        public int sType { get; set; }
        public string sCode { get; set; }
        public string sDescription { get; set; }
        public bool status { get; set; }
        public int sCreatedBy { get; set; }
        public string command { get; set; }
        public int CurrentStatus { get; set; }
        public int sSenderIDType { get; set; }
        public int sShortCodeType { get; set; }
        public bool sDndByPass { get; set; }
        public string rejectnote { get; set; }
        public int ShortCodeType { get; set; }
        public string UserIp { get; set; }
        public string Category { get; set; }
    }

    public class AorRSIDSC
    {
        public int id { get; set; }
        public int currentstatus { get; set; }
        public string rejectnote { get; set; }
        public int updatedby { get; set; }

    }

}
