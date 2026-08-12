using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.MailBox
{
    public class UserMailBoxMappingRequest
    {
        public int NUSERMAILBOXMAPPINGID { get; set; }
        public int NSTATUS { get; set; }
    }
    public class InsertUserMailboxMappingRequest
    {
        public int NCUSTOMERID { get; set; }
        public int NUSERID { get; set; }
        public int NMBCID { get; set; }
        public int NSENDERID { get; set; }
        public int NLANG { get; set; }
        public int NMAILBOXID { get; set; }
        public string STRFORMATIDS { get; set; }
        public int NAUTHREQUIRED { get; set; }
        public string STRUSERNAME { get; set; }
        public string STRPASSWORD { get; set; }
        public int NDLRREQUIRED { get; set; }
        public int NLIMITCREDITS { get; set; }
        public int NMAXCREDITS { get; set; }
        public int NLIMITRECEPIENTS { get; set; }
        public int NMAXRECEPIENTS { get; set; }
        public int NLIMITNOTIFICATIONS { get; set; }
        public int NMAXNOTIFICATIONS { get; set; }
        public int NNOTIFYDURATION { get; set; }
        public int NNOOFRETRIES { get; set; }
        public bool NUSETAGS { get; set; }
        public string STRSTARTTAG { get; set; }
        public string STRENDTAG { get; set; }
        public int NADDEDBY { get; set; }
        public int NUPDATEDBY { get; set; }
        public int NUSERMAILBOXMAPPINGID { get; set; }
        public int NRETVAL { get; set; }
        public string command { get; set; }
    }

    public class UserbyCustomerIdReq
    {
        public int NCUSTID { get; set; }
    }

    public class SenderbyUserIdReq
    {
        public int NUSERID { get; set; }
    }
}
