using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.DBAlerts
{
   public class DBAlertsRequest
    {
       public int NALERTID { get; set; }
       public int NUSERID { get; set; }
       public int NSTATUS { get; set; }
       public int NRETURN { get; set; }
    }

   public class SetAlertsReq
   {
       public string? STRALERTNAME { get; set; }
       public string? STRDESCRIPTION { get; set; }
       public int NSENDERID { get; set; }
       public int NRULEID { get; set; }
       public string? STRMOBILEFLD { get; set; }
       public string? STRPLACEHOLDERS { get; set; }
       public int NLANGUAGE { get; set; }
       public string? STRMESSAGE { get; set; }
       public string? STRCUSTOMMSG { get; set; }
       public int NDLRREQUIRED { get; set; }
       public string? STRSCHEDULE { get; set; }
       public int NCREATEDBY { get; set; }
       public string? STRFIELD1 { get; set; }
       public string? STRFIELD2 { get; set; }
       public string? STRFIELD3 { get; set; }
       public int NRETURN { get; set; }
       public int NID { get; set; }
       public string? command { get; set; }
       public int NALERTID { get; set; }
       public int NUPDATEDBY { get; set; }
       public int CURRENTSTATUS { get; set; }
   
   }
}
