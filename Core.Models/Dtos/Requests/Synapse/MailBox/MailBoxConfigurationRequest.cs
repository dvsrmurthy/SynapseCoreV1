using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.MailBox
{
   public class MailBoxConfigurationRequest
    {

        public int NMAILBOXID { get; set; }
        public int NSTATUS { get; set; }
    }
   public class AddMailBoxConfiguration
   {
       public string STRHOST { get; set; }
       public string STRMAILBOX { get; set; }
       public string STRPASSWORD { get; set; }
       public int NPORT { get; set; }
       public bool NSSL { get; set; }
       public int NFREQUENCY { get; set; }
       public int NMAILTYPE { get; set; }
       public int NSTATUS { get; set; }
       public int NADDEDBY { get; set; }
       public int NRETVAL { get; set; }
       public int NMBID { get; set; }
       public string command { get; set; }
       public int NUPDATEDBY { get; set; }
       public int NMAILBOXID { get; set; }


   }
   public class CheckerMailconfigRequest
   {
       public int ID { get; set; }
       public int CURRENTSTATUS { get; set; }
       public int RETURNVALUE { get; set; }
       public int UPDATEDBY { get; set; }
       public string REJECTNOTE { get; set; }
   }
}
